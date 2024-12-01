using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BlueGraph.Editor
{
    /// <summary>
    /// Build a basic window container for the BlueGraph canvas
    /// </summary>
    public class GraphEditorWindow : EditorWindow
    {
        public VisualTreeAsset graphViewTreeAsset;
        private VisualElement mainLayout;
        public CanvasView Canvas { get; protected set; }
        public hCustomGraph Graph { get; protected set; }
        public static GraphEditorWindow window;
        string filterText;

        // * Visual Elements
        private VisualElement graphViewContainer; 
        private VisualElement toolboxVisualElement;
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
            
            this.Graph = graph;
            VisualElement root = rootVisualElement;
            graphViewTreeAsset.CloneTree(root);

            this.graphViewContainer = root.Q<VisualElement>("_graphField");
            this.toolboxViewContainer = root.Q<ScrollView>("_toolBoxContainer");
            this.toolboxVisualElement = root.Q<VisualElement>("_toolBox");
            this.mainLayout = root.Q<VisualElement>("_mainLayout");
            this.namespaceListView = root.Q<ListView>("_namespaceListView");
            this.toolbarSearchField = root.Q<ToolbarSearchField>("_toolbarSearchField");
            this.toolbarBreadcrumbs = root.Q<ToolbarBreadcrumbs>("_toolbarBreadCrumbs");

            TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
            splitView.Add(toolboxVisualElement);
            splitView.Add(graphViewContainer);
            mainLayout.Add(splitView);
            splitView.StretchToParentSize();

            Load(this.Graph);
        }

        public virtual void Load(hCustomGraph graph)
        {
            Canvas = new CanvasView(this);
            Canvas.Load(graph);
            this.graphViewContainer.Add(Canvas);
            Canvas.StretchToParentSize();
            ReadGraph();
        }
        public void ReadGraph()
        {
            // * Namespaces
            namespaceListView.headerTitle = "Namespaces";
            namespaceListView.itemsSource = this.Graph.ParsedScript.AllNamespaces;
            namespaceListView.makeItem = () =>
            {
                TextField textField = new TextField();
                textField.isReadOnly = false;
                return textField;
            };
            namespaceListView.bindItem = (element, i) =>
            {
                (element as TextField).value = this.Graph.ParsedScript.AllNamespaces[i];
            };
            namespaceListView.Rebuild();

            string firstClass = this.Graph.ParsedScript.ClassNames.FirstOrDefault();

            // * Foldout
            // Create main foldouts for fields, properties, and methods
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
            ParsedFieldList fieldInfos = this.Graph.ParsedScript.ClassFields[firstClass];
            ParsedMethodList methodInfos = this.Graph.ParsedScript.ClassMethods[firstClass];

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


        Dictionary<string, Foldout> GroupFieldsByNamespaces(ParsedFieldList fieldInfos)
        {
            Dictionary<string, Foldout> fieldNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (var field in fieldInfos.Data)
            {
                string namespaceName = field.RootNamespace ?? "Global Namespace";
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
        Dictionary<string, Foldout> GroupMethodsByNamespaces(ParsedMethodList methods)
        {
            Dictionary<string, Foldout> methodNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (var method in methods.Data)
            {
                string namespaceName = method.RootNamespace ?? "Global Namespace";
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
                    }
                ) { text = method.Name });

                methodNamespaceFoldouts[namespaceName].Add(methodButton);
            }
            return methodNamespaceFoldouts;
        }
    }
}
