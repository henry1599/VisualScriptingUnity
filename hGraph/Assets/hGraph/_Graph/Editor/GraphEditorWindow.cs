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
            public eParsedDataType Type;
            public string Name;
            public override bool Equals(object obj)
            {
                return obj is GraphSelection selection &&
                       Type == selection.Type &&
                       Name == selection.Name;
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
        GraphSelection currentSelection;
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
            this.currentSelection = new GraphSelection() 
            {
                Type = firstGraph.Category, 
                Name = firstGraph.Name
            };
        
            // this.toolbarBreadcrumbs.Clear();

            this.selectionStack ??= new Stack<GraphSelection>();
            this.selectionStack.Clear();
            SelectItem(this.currentSelection);

            Load(this.ActiveGraph);
        }
        
        public void SelectItem(GraphSelection selection)
        {
            if (this.selectionStack.Count == 0)
            {
                this.selectionStack.Push(this.currentSelection);
                this.toolbarBreadcrumbs.PushItem(this.currentSelection.Name, () => SelectItem(this.currentSelection));
                return;
            }
            while (this.selectionStack.Count > 0)
            {
                this.currentSelection = this.selectionStack.Peek();
                if (this.currentSelection.Equals(selection))
                {
                    break;
                }
                else if (IsChildOf(selection, this.currentSelection))
                {
                    this.selectionStack.Push(selection);
                    this.toolbarBreadcrumbs.PushItem(selection.Name, () => SelectItem(selection));
                    break;
                }
                else
                {
                    this.selectionStack.Pop();
                    this.toolbarBreadcrumbs.PopItem();
                }
            }
            if (this.selectionStack.Count == 0)
            {
                this.selectionStack.Push(this.currentSelection);
                this.toolbarBreadcrumbs.PushItem(this.currentSelection.Name, () => SelectItem(this.currentSelection));
            }
        }
        public bool IsChildOf(GraphSelection childTest, GraphSelection parentTest)
        {
            if (childTest.Type == eParsedDataType.Method && parentTest.Type == eParsedDataType.Class)
            {
                return this.ActiveGraph.ParsedScript.Classes[parentTest.Name].Methods.Any(m => m.Name == childTest.Name && m.Parent.Name == parentTest.Name);
            }
            if (childTest.Type == eParsedDataType.Field && parentTest.Type == eParsedDataType.Class)
            {
                return this.ActiveGraph.ParsedScript.Classes[parentTest.Name].Fields.Any(f => f.Name == childTest.Name && f.Parent.Name == parentTest.Name);
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
            graph.BuildGraph(this.currentSelection.Type, this.currentSelection.Name);
            Canvas = new CanvasView(this);
            Canvas.Load(graph);
            this.graphViewContainer.Add(Canvas);
            Canvas.StretchToParentSize();
            ReadCurrentGraph(graph);
        }
        public void ReadCurrentGraph(hCustomGraph graph)
        {
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
            List<ParsedField> fieldInfos = graph.ParsedScript.Classes[this.currentSelection.Name].Fields;
            List<ParsedMethod> methodInfos = graph.ParsedScript.Classes[this.currentSelection.Name].Methods;

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
                        SelectItem(new GraphSelection() { Type = eParsedDataType.Method, Name = method.Name });
                    }
                ) { text = method.Name });

                methodNamespaceFoldouts[namespaceName].Add(methodButton);
            }
            return methodNamespaceFoldouts;
        }
    }
}
