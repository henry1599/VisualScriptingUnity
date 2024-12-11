using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using System.Text;
// using UnityEditor.IMGUI.Controls;

public class ProjectView : EditorWindow 
{
    public VisualTreeAsset treeAsset;
    private static ProjectView window;
    Button addFolderButton;
    VisualElement mainContainer;
    VisualElement menuView;
    ScrollView contentView;
    VisualElement scrollContent;
    VisualElement contentGroupView;
    Button firstPageButton;
    Button lastPageButton;
    Button nextPageButton;
    Button previousPageButton;
    Label pageInfoLabel;
    Slider itemSizeSlider;
    Slider itemPerPageSlider;
    ToolbarBreadcrumbs toolbarBreadcrumbs;
    TreeView treeView;
    string chosenFolder;

    // * Pagination
    private int currentPage = 0;
    private float minItemIconSize = 40;
    private int itemsPerPage = 20;
    private List<string> currentContent = new List<string>();
    private float itemSize = 40;
    private List<VisualElement> treeViewItems = new List<VisualElement>();

    [MenuItem("Window/CustomProjectView")]
    static void Init()
    {
        window = GetWindow<ProjectView>("Project View - Custom"); 
        window.minSize = new Vector2(500, 300);
    }
    void OnEnable()
    {
        VisualElement root = rootVisualElement;
        treeAsset.CloneTree(root);
        GatherElements();
    }
    void GatherElements()
    {
        this.addFolderButton = this.rootVisualElement.Q<Button>("_addFolderButton");
        this.menuView = this.rootVisualElement.Q<VisualElement>("_menuView");
        this.contentView = this.rootVisualElement.Q<ScrollView>("_contentView");
        this.treeView = this.rootVisualElement.Q<TreeView>("_treeView");
        this.mainContainer = this.rootVisualElement.Q<VisualElement>("_mainContainer");
        this.contentGroupView = this.rootVisualElement.Q<VisualElement>("_contentGroupView");
        this.firstPageButton = this.rootVisualElement.Q<Button>("_firstPageButton");
        this.lastPageButton = this.rootVisualElement.Q<Button>("_lastPageButton");
        this.nextPageButton = this.rootVisualElement.Q<Button>("_nextPageButton");
        this.previousPageButton = this.rootVisualElement.Q<Button>("_prevPageButton");
        this.pageInfoLabel = this.rootVisualElement.Q<Label>("_pageInfo");
        this.itemSizeSlider = this.rootVisualElement.Q<Slider>("_itemSizeSlider");
        this.itemPerPageSlider = this.rootVisualElement.Q<Slider>("_itemPerPageSlider");
        this.toolbarBreadcrumbs = this.rootVisualElement.Q<ToolbarBreadcrumbs>("_pathBreadcrumbs");
        this.scrollContent = this.contentView
            ?.Q<VisualElement>("unity-content-and-vertical-scroll-container")
            ?.Q<VisualElement>("unity-content-viewport")
            ?.Q<VisualElement>("unity-content-container");

        if (this.scrollContent != null)
            this.scrollContent.style.flexWrap = Wrap.Wrap;

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        splitView.Add(this.menuView);
        splitView.Add(this.contentGroupView);
        this.mainContainer.Add(splitView);
        splitView.StretchToParentSize();

        this.addFolderButton.clicked += OnAddFolderButtonClicked;
        this.treeView.selectionChanged += OnTreeViewSelectionChanged;
        this.firstPageButton.clicked += OnFirstPageButtonClicked;
        this.lastPageButton.clicked += OnLastPageButtonClicked;
        this.nextPageButton.clicked += OnNextPageButtonClicked;
        this.previousPageButton.clicked += OnPreviousPageButtonClicked;
        this.itemSizeSlider.RegisterValueChangedCallback(evt => OnItemSizeChanged(evt.newValue));
        this.itemPerPageSlider.RegisterValueChangedCallback(evt => OnItemPerPageChanged(evt.newValue));

        this.itemSize = this.itemSizeSlider.value;
        this.itemsPerPage = (int)this.itemPerPageSlider.value;
        
        ReloadTreeView();

        OnItemSizeChanged(this.itemSizeSlider.value);
        OnItemPerPageChanged(this.itemPerPageSlider.value);
    }
    private void UpdateBreadcrumbs(string folderPath)
    {
        toolbarBreadcrumbs.Clear();
        string[] pathParts = folderPath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
        string currentPath = string.Empty;

        foreach (var part in pathParts)
        {
            currentPath = string.IsNullOrEmpty(currentPath) ? part : Path.Combine(currentPath, part);
            string pathForBreadcrumb = currentPath;
            toolbarBreadcrumbs.PushItem(part, () => TapOnTreeViewItem(pathForBreadcrumb));
        }
    }
    void TapOnTreeViewItem(string folder)
    {
        this.treeView.selectedIndex = this.treeViewItems.FindIndex(item => item.Q<Label>().text == folder);
        this.treeView.ScrollToItem(this.treeView.selectedIndex);
        OnFirstPageButtonClicked();
        DisplayFolderContents(folder);
    }
    private void OnItemPerPageChanged(float newValue)
    {
        this.itemsPerPage = (int)newValue;
        OnFirstPageButtonClicked();
    }

    bool IsListItemSize()
    {
        return this.itemSize <= this.minItemIconSize;
    }
    private void OnItemSizeChanged(float newSize)
    {
        itemSize = newSize;
        if (itemSize <= this.minItemIconSize)
        {
            this.scrollContent.style.flexDirection = FlexDirection.Row;
        }
        else
        {
            this.scrollContent.style.flexDirection = FlexDirection.Row;
        }
        DisplayCurrentPage();
    }
    private void OnFirstPageButtonClicked()
    {
        this.currentPage = 0;
        DisplayCurrentPage();
    }

    private void OnLastPageButtonClicked()
    {
        this.currentPage = currentContent.Count / itemsPerPage;
        DisplayCurrentPage();
    }

    private void OnNextPageButtonClicked()
    {
        this.currentPage++;
        this.currentPage = Mathf.Min(this.currentPage, currentContent.Count / itemsPerPage);
        DisplayCurrentPage();
    }

    private void OnPreviousPageButtonClicked()
    {
        this.currentPage--;
        this.currentPage = Mathf.Max(this.currentPage, 0);
        DisplayCurrentPage();
    }

    void ReloadTreeView()
    {
        if (string.IsNullOrEmpty(chosenFolder))
        {
            return;
        }

        List<TreeViewItemData<TreeElement>> items = new List<TreeViewItemData<TreeElement>>();
        TreeViewItemData<TreeElement> root = new TreeViewItemData<TreeElement>(chosenFolder.GetHashCode(), new TreeElement()
        {
            Path = chosenFolder,
            DisplayName = Path.GetFileName(chosenFolder)
        }, new List<TreeViewItemData<TreeElement>>());
        
        AddFoldersToTree(chosenFolder, (List<TreeViewItemData<TreeElement>>)root.children, 1);
        items.Add(root);

        treeView.SetRootItems(items);
        treeView.Rebuild();

        OnFirstPageButtonClicked();
        OnItemSizeChanged(itemSize);
        UpdateTreeItems();
        DisplayCurrentPage();
    }
    void UpdateTreeItems()
    {   
        this.treeViewItems = GetTreeViewVisualElementItems();
        if (this.treeViewItems == null)
        {
            return;
        }

        foreach (var item in this.treeViewItems)
        {
            VisualElement labelContent = item?.Q<VisualElement>("unity-tree-view__item-content");
            Label label = labelContent
                            ?.Q<Label>();
            Toggle toggle = item
                            ?.Q<VisualElement>("unity-tree-view__item-toggle")
                            ?.Q<Toggle>();
            toggle.RegisterCallback<ChangeEvent<bool>>(evt => UpdateTreeItems());
            if (label != null)
            {
                label.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
                label.style.unityFontStyleAndWeight = FontStyle.Bold;
                label.style.unityTextAlign = TextAnchor.MiddleLeft;
            }
            string folderIconName = "d_Folder Icon";
            if (toggle.value)
            {
                folderIconName = "d_FolderOpened Icon";
            }
            var folderIcon = EditorGUIUtility.IconContent(folderIconName).image;
            Image icon = new Image 
            { 
                image = folderIcon,
            };
            icon.style.height = new StyleLength(new Length(20, LengthUnit.Pixel));
            icon.style.width = new StyleLength(new Length(20, LengthUnit.Pixel));
            
            labelContent.style.flexDirection = FlexDirection.Row;
            if (labelContent.Q<Image>() == null)
                labelContent.Insert(0, icon);
            else
                labelContent.Q<Image>().image = folderIcon;
        }
    }

    // * Use debugger and reflection in Editor to view the element ids
    List<VisualElement> GetTreeViewVisualElementItems()
    {
        return

        this.treeView
            ?.Q<ScrollView>()
            ?.Q<VisualElement>("unity-content-and-vertical-scroll-container")
            ?.Q<VisualElement>("unity-content-viewport")
            ?.Q<VisualElement>("unity-content-container")
            ?.Children()
            ?.ToList();
    }

    private void AddFoldersToTree(string path, List<TreeViewItemData<TreeElement>> items, int depth)
    {
        var directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            if (directory.EndsWith(".meta"))
            {
                continue;
            }
            var item = new TreeViewItemData<TreeElement>(directory.GetHashCode(), new TreeElement()
            {
                Path = directory,
                DisplayName = Path.GetFileName(directory)
            }, new List<TreeViewItemData<TreeElement>>());
            items.Add(item);
            AddFoldersToTree(directory, (List<TreeViewItemData<TreeElement>>)item.children, depth + 1);
        }
    }
    private void OnAddFolderButtonClicked()
    {
        AddFolder();
        ReloadTreeView();
    }
    void AddFolder()
    {
        string folderPath = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
        if (!string.IsNullOrEmpty(folderPath))
        {
            if (folderPath.StartsWith(Application.dataPath))
            {
                this.chosenFolder = "Assets" + folderPath.Replace(Application.dataPath, "").Replace('\\', '/');
            }
            else
            {
                Debug.LogWarning("Please select a folder inside the Unity project.");
            }
        }
    }
    private void OnTreeViewSelectionChanged(IEnumerable<object> selectedItems)
    {
        foreach (var item in selectedItems)
        {
            if (item is TreeElement element)
            {
                OnFirstPageButtonClicked();
                DisplayFolderContents(element.Path);
                UpdateBreadcrumbs(element.Path);
            }
        }
        UpdateTreeItems();  
    }
    private void DisplayFolderContents(string folderPath)
    {
        currentContent.Clear();
        currentContent.AddRange(Directory.GetDirectories(folderPath).Where(path => !path.EndsWith(".meta")));
        currentContent.AddRange(Directory.GetFiles(folderPath).Where(path => !path.EndsWith(".meta")));

        DisplayCurrentPage();
        UpdateBreadcrumbs(folderPath);
    }
    private void DisplayCurrentPage()
    {
        this.pageInfoLabel.text = $"{currentPage + 1} of {currentContent.Count / itemsPerPage + 1}";
        contentView.Clear();

        int startIndex = currentPage * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, currentContent.Count);

        for (int i = startIndex; i < endIndex; i++)
        {

            var itemPath = currentContent[i];
            var itemName = Path.GetFileName(itemPath);

            // Create a button with icon and text
            var button = new Button()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    justifyContent = Justify.FlexStart,
                    marginBottom = 5,
                    backgroundColor = new StyleColor(new Color(0.2f, 0.2f, 0.2f)),
                    width = itemSize,
                    height = itemSize,
                    overflow = Overflow.Hidden
                }
            };


            // Add icon
            var icon = new Image()
            {
                tooltip = itemName
            };
            icon.image = GetIconForPath(itemPath);
            icon.style.marginRight = 5;
            icon.style.width = Mathf.Max(this.itemSize, this.minItemIconSize);
            icon.style.height = Mathf.Max(this.itemSize, this.minItemIconSize);
            icon.RegisterCallback<MouseUpEvent>(evt => OnItemClicked(itemPath));
            button.Add(icon);

            // Add text
            var label = new Label(itemName)
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleLeft,
                    overflow = Overflow.Hidden,
                },
                tooltip = itemName
            };
            button.Add(label);
            if (IsListItemSize())
            {
                button.style.flexDirection = FlexDirection.Row;
                button.style.width = this.scrollContent.layout.width - 20;
                button.style.height = this.minItemIconSize;
            }
            else
            {
                button.style.flexDirection = FlexDirection.Column;
                button.style.width = this.itemSize;
            }

            button.style.borderBottomColor = 
            button.style.borderTopColor =
            button.style.borderLeftColor =
            button.style.borderRightColor = new StyleColor(new Color(70f / 255f, 70f / 255f, 70f / 255f));

            button.style.borderBottomWidth = 
            button.style.borderTopWidth =
            button.style.borderLeftWidth =
            button.style.borderRightWidth = 2;

            button.style.borderTopLeftRadius = 
            button.style.borderTopRightRadius =
            button.style.borderBottomLeftRadius =
            button.style.borderBottomRightRadius = 10;



            contentView.Add(button);
            contentView.MarkDirtyRepaint();
        }
    }
    private Texture GetIconForPath(string path)
    {
        if (Directory.Exists(path))
        {
            return EditorGUIUtility.IconContent("Folder Icon").image;
        }

        string extension = Path.GetExtension(path).ToLower();
        switch (extension)
        {
            case ".cs":
                return EditorGUIUtility.IconContent("cs Script Icon").image;
            case ".shader":
                return EditorGUIUtility.IconContent("Shader Icon").image;
            case ".mat":
                return EditorGUIUtility.IconContent("Material Icon").image;
            case ".prefab":
                return EditorGUIUtility.IconContent("Prefab Icon").image;
            case ".png":
            case ".jpg":
            case ".jpeg":
                return LoadImageThumbnail(path);
            case ".asset":
                return EditorGUIUtility.IconContent("ScriptableObject Icon").image;
            default:
                return EditorGUIUtility.IconContent("DefaultAsset Icon").image;
        }
    }
    private Texture LoadImageThumbnail(string path)
    {
        var asset = AssetDatabase.LoadAssetAtPath<Texture2D>(path); 
        if (asset != null)
        {
            Texture2D preview = AssetPreview.GetAssetPreview(asset);
            if (preview != null)
                return preview;
        }
        return EditorGUIUtility.IconContent("Texture Icon").image;
    }
    private void OnItemClicked(string itemPath)
    {
        if (Directory.Exists(itemPath))
        {
             DisplayFolderContents(itemPath);
        }
        else
        {
            // Handle file click if needed
            Debug.Log("File clicked: " + itemPath);
        }
    }

    public class TreeElement
    {
        public string Path;
        public string DisplayName;
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
