using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Graph = BlueGraph.Graph;

namespace BlueGraph.Editor
{
    /// <summary>
    /// Build a basic window container for the BlueGraph canvas
    /// </summary>
    public class GraphEditorWindow : EditorWindow
    {
        public class GraphSelection 
        {
            public ParsedObject Item;
            public GraphSelection() {}
            public GraphSelection(GraphSelection selection)
            {
                Item = selection.Item;
            }
            public override bool Equals(object obj)
            {
                return Item.Equals(obj);
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
        public VisualTreeAsset graphViewTreeAsset;
        private VisualElement mainLayout;
        public CanvasView Canvas { get; protected set; }
        public hCustomGraph ActiveGraph { get; protected set; }
        public static GraphEditorWindow window;
        string filterText;
        GraphSelection currentSelection
        {
            get => this._currentSelection;
            set
            {
                if (this._currentSelection != value)
                {
                    this.ActiveGraph.BuildGraph(value.Item);
                    this._currentSelection = value;
                    Load(this.ActiveGraph);
                    return;
                }
                this._currentSelection = value;
            }
        } private GraphSelection _currentSelection = null;
        Stack<GraphSelection> selectionStack = new Stack<GraphSelection>();

        // * Visual Elements
        private VisualElement graphViewContainer; 
        private VisualElement toolboxVisualElement;
        private VisualElement bottomMainLayout;
        private ScrollView toolboxViewContainer;
        private ListView namespaceListView;
        private ToolbarSearchField toolbarSearchField;
        private ToolbarBreadcrumbs toolbarBreadcrumbs;

        public static void OpenGraphEditorWindow(hCustomGraph graph)
        {
            
            window = GetWindow<GraphEditorWindow>("Graph Editor"); 
            window.minSize = new Vector2(800, 600);
            window.Initialize(graph);
        }
        private void Initialize(hCustomGraph graph)
        {
            if (this.graphViewTreeAsset == null)
                return;
            
            this.ActiveGraph = graph;
            VisualElement root = rootVisualElement;
            graphViewTreeAsset.CloneTree(root);

            this.graphViewContainer = root.Q<VisualElement>("_graphField");
            this.toolboxViewContainer = root.Q<ScrollView>("_toolBoxContainer");
            this.toolboxVisualElement = root.Q<VisualElement>("_toolBox");
            this.mainLayout = root.Q<VisualElement>("_mainLayout");
            this.namespaceListView = root.Q<ListView>("_namespaceListView");
            this.toolbarSearchField = root.Q<ToolbarSearchField>("_toolbarSearchField");
            this.toolbarBreadcrumbs = root.Q<ToolbarBreadcrumbs>("_toolbarBreadCrumbs");
            this.bottomMainLayout = root.Q<VisualElement>("_bottomMainLayout");

            TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
            splitView.Add(toolboxVisualElement);
            splitView.Add(graphViewContainer);
            this.bottomMainLayout.Add(splitView);
            splitView.StretchToParentSize();

            // * Get first class
            var firstGraph = this.ActiveGraph.ParsedScript.Classes.Values.First();
        
            // this.toolbarBreadcrumbs.Clear();

            this.selectionStack ??= new Stack<GraphSelection>();
            this.selectionStack.Clear();
            SelectItem(new GraphSelection() 
            {
                Item = firstGraph
            });

            // Load(this.ActiveGraph);
        }
        
        public void SelectItem(GraphSelection selection)
        {
            if (selection == null || selection.Item == null)
            {
                Debug.LogWarning("Selection or Selection Item is null.");
                return;
            }

            Debug.Log($"SelectItem called with selection: {selection.Item.Name}");

            // If the stack is empty, push the new selection
            if (this.selectionStack.Count == 0)
            {
                Debug.Log("Selection stack is empty. Pushing the new selection.");
                this.currentSelection = selection;
                this.selectionStack.Push(selection);
                this.toolbarBreadcrumbs.PushItem(selection.Item.Name, () => SelectItem(new (selection)));
                return;
            }

            // Check if the new selection is already the current selection
            if (this.currentSelection.Equals(selection))
            {
                Debug.Log("New selection is already the current selection.");
                return;
            }

            // Check if the new selection is a child of the current selection
            if (IsChildOf(selection, this.currentSelection))
            {
                Debug.Log("New selection is a child of the current selection. Pushing the new selection.");
                this.currentSelection = selection;
                this.selectionStack.Push(selection);
                this.toolbarBreadcrumbs.PushItem(selection.Item.Name, () => SelectItem(new (selection)));
            }
            else
            {
                Debug.Log("New selection is not a child of the current selection. Popping the stack until we reach the selection.");

                // Pop items until we find the selection or the stack is empty
                while (this.selectionStack.Count > 0)
                {
                    var top = this.selectionStack.Peek();
                    if (top.Equals(selection))
                    {
                        break;
                    }
                    this.selectionStack.Pop();
                    this.toolbarBreadcrumbs.PopItem();
                }

                // If the stack is empty, it means the selection was not found in the stack
                if (this.selectionStack.Count == 0)
                {
                    Debug.LogWarning("Selection not found in the stack. Pushing the new selection.");
                    this.currentSelection = selection;
                    this.selectionStack.Push(selection);
                    this.toolbarBreadcrumbs.PushItem(selection.Item.Name, () => SelectItem(new (selection)));
                }
                else
                {
                    // The selection was found in the stack
                    Debug.Log("Selection found in the stack. Setting it as the current selection.");
                    this.currentSelection = this.selectionStack.Peek();
                }
            }
        }
        public bool IsChildOf(GraphSelection childTest, GraphSelection parentTest)
        {
            if (childTest.Item.Category == eParsedDataType.Method && parentTest.Item.Category == eParsedDataType.Class)
            {
                return this.ActiveGraph.ParsedScript.Classes[parentTest.Item.Name].Methods.Any(m => m == childTest.Item);
            }
            if (childTest.Item.Category == eParsedDataType.Field && parentTest.Item.Category == eParsedDataType.Class)
            {
                return this.ActiveGraph.ParsedScript.Classes[parentTest.Item.Name].Fields.Any(f => f == childTest.Item);
            }
            // * Check if a field is a child (local var) of a method
            // * Check if a field is a child (parameter) of a method
            // * Check if a field is a child of a class (global var)
            // if (childTest.Type == eParsedDataType.Field && parentTest.Type == eParsedDataType.Method)
            // {
            //     return this.ActiveGraph.ParsedScript.Classes[parentTest.Name].Methods.Any(m => m.Params.Any(p => p.Name == childTest.Name));
            // }
            return false;
        }

        public virtual void Load(hCustomGraph graph)
        {
            // graph.BuildGraph(this.currentSelection.Type, this.currentSelection.Name);
            Canvas = new CanvasView(this);
            Canvas.Load(graph);
            this.graphViewContainer.Clear();
            this.graphViewContainer.Add(Canvas);
            Canvas.StretchToParentSize();
            ReadCurrentGraph(graph, this.currentSelection);
        }
        public void ReadCurrentGraph(hCustomGraph graph, GraphSelection selection)
        {
            switch (selection.Item.Category)
            {
                case eParsedDataType.Class:
                    ReadClassPerspective(graph, selection);
                    break;
                case eParsedDataType.Method:
                    ReadMethodPerspective(graph, selection);
                    break;
            }
        }

        void ReadClassPerspective(hCustomGraph graph, GraphSelection selection)
        {
            this.toolboxViewContainer.Clear();

            // * Namespaces
            namespaceListView.headerTitle = "Namespaces";
            namespaceListView.itemsSource = graph.ParsedScript.AllNamespaces;
            namespaceListView.makeItem = () =>
            {
                TextField textField = new TextField
                {
                    isReadOnly = true
                };
                return textField;
            };
            namespaceListView.bindItem = (element, i) =>
            {
                (element as TextField).value = graph.ParsedScript.AllNamespaces[i];
            };
            namespaceListView.Rebuild();

            // * Foldout
            Foldout variableFoldout = new Foldout
            {
                style =
                    {
                        borderTopColor = Color.black,
                        borderTopWidth = 1,
                        borderLeftColor = Color.black,
                        borderLeftWidth = 1,
                        borderRightColor = Color.black,
                        borderRightWidth = 1
                    }
            };
            Foldout functionFoldout = new Foldout
            {
                style =
                    {
                        borderBottomColor = Color.black,
                        borderBottomWidth = 1,
                        borderLeftColor = Color.black,
                        borderLeftWidth = 1,
                        borderRightColor = Color.black,
                        borderRightWidth = 1
                    }
            };
            // Dictionaries to store namespace foldouts
            ParsedClass parsedClass = selection.Item as ParsedClass;
            if (parsedClass == null)
                return;
            List<ParsedField> fieldInfos = parsedClass.Fields;
            List<ParsedMethod> methodInfos = parsedClass.Methods;

            Dictionary<string, Foldout> fieldNamespaceFoldouts = GroupFieldsByNamespaces(fieldInfos);
            Dictionary<string, Foldout> methodNamespaceFoldouts = GroupMethodsByNamespaces(methodInfos);

            // Add namespace foldouts to main foldouts
            int fieldCount = fieldNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            variableFoldout.text = $"<b>Variables ({fieldCount})</b>";
            foreach (var foldout in fieldNamespaceFoldouts.Values)
            {
                variableFoldout.Add(foldout);
            }

            int methodCount = methodNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            functionFoldout.text = $"<b>Functions ({methodCount})</b>";
            foreach (var foldout in methodNamespaceFoldouts.Values)
            {
                functionFoldout.Add(foldout);
            }

            // Clear the toolbox container and add the main foldouts
            toolboxViewContainer.Add(variableFoldout);
            toolboxViewContainer.Add(functionFoldout);
        }
        void ReadMethodPerspective(hCustomGraph graph, GraphSelection selection)
        {
            this.toolboxViewContainer.Clear();

            // * Namespaces
            namespaceListView.headerTitle = "Namespaces";
            namespaceListView.itemsSource = graph.ParsedScript.AllNamespaces;
            namespaceListView.makeItem = () =>
            {
                TextField textField = new TextField
                {
                    isReadOnly = true
                };
                return textField;
            };
            namespaceListView.bindItem = (element, i) =>
            {
                (element as TextField).value = graph.ParsedScript.AllNamespaces[i];
            };
            namespaceListView.Rebuild();

            // * Foldout
            Foldout variableFoldout = new Foldout
            {
                style =
                    {
                        borderTopColor = Color.black,
                        borderTopWidth = 1,
                        borderLeftColor = Color.black,
                        borderLeftWidth = 1,
                        borderRightColor = Color.black,
                        borderRightWidth = 1
                    }
            };
            Foldout functionFoldout = new Foldout
            {
                style =
                    {
                        borderBottomColor = Color.black,
                        borderBottomWidth = 1,
                        borderLeftColor = Color.black,
                        borderLeftWidth = 1,
                        borderRightColor = Color.black,
                        borderRightWidth = 1
                    }
            };
            // Dictionaries to store namespace foldouts
            ParsedMethod method = selection.Item as ParsedMethod;
            if (method == null)
                return;
            List<ParsedField> fieldInfos = method.Params;

            Dictionary<string, Foldout> fieldNamespaceFoldouts = GroupFieldsByNamespaces(fieldInfos);

            // Add namespace foldouts to main foldouts
            int fieldCount = fieldNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            variableFoldout.text = $"<b>Local variables ({fieldCount})</b>";
            foreach (var foldout in fieldNamespaceFoldouts.Values)
            {
                variableFoldout.Add(foldout);
            }

            // Clear the toolbox container and add the main foldouts
            if (variableFoldout.childCount > 0)
                toolboxViewContainer.Add(variableFoldout);
            if (functionFoldout.childCount > 0)
                toolboxViewContainer.Add(functionFoldout);
        }


        Dictionary<string, Foldout> GroupFieldsByNamespaces(List<ParsedField> fieldInfos)
        {
            Dictionary<string, Foldout> fieldNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (var field in fieldInfos)
            {
                string namespaceName = string.IsNullOrEmpty(field.RootNamespace) ? "Global Namespace" : field.RootNamespace;
                if (!fieldNamespaceFoldouts.ContainsKey(namespaceName))
                {
                    fieldNamespaceFoldouts[namespaceName] = new Foldout() 
                    { 
                        text = $"<b>{namespaceName}</b>",
                        value = false
                    };
                }
                var icon = EditorGUIUtility.IconContent("d_AreaEffector2D Icon").image;
                string displayName = ObjectNames.NicifyVariableName(field.Name);
                Button fieldButton = Common.CreateButtonWithIcon(icon, new Button() { text = displayName });
                fieldNamespaceFoldouts[namespaceName].Add(fieldButton);
            }
            return fieldNamespaceFoldouts;
        }
        Dictionary<string, Foldout> GroupMethodsByNamespaces(List<ParsedMethod> methods)
        {
            Dictionary<string, Foldout> methodNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (var method in methods)
            {
                string namespaceName = string.IsNullOrEmpty(method.RootNamespace) ? "Global Namespace" : method.RootNamespace;
                if (!methodNamespaceFoldouts.ContainsKey(namespaceName))
                {
                    methodNamespaceFoldouts[namespaceName] = new Foldout() 
                    { 
                        text = $"<b>{namespaceName}</b>",
                        value = false 
                    };
                }
                var icon = EditorGUIUtility.IconContent("d_Tile Icon").image;
                Button methodButton = Common.CreateButtonWithIcon(icon, new Button(
                    () => 
                    {
                        SelectItem(new GraphSelection() { Item = method });
                    }
                ) { text = method.Name });

                methodNamespaceFoldouts[namespaceName].Add(methodButton);
            }
            return methodNamespaceFoldouts;
        }
    }
}
