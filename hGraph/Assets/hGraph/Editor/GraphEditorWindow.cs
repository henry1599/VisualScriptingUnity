using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace hGraph.Editor
{
    public class GraphEditorWindow : EditorWindow
    {
        private const string EditorPrefsKey = "GraphEditorWindow_hBehaviour";
        public VisualTreeAsset graphViewTreeAsset;
        private CustomGraphView graphView;
        private Button loadButton;

        private VisualElement mainLayout;
        private Label classNameLabel;
        private VisualElement graphViewContainer; 
        private VisualElement toolboxVisualElement;
        private ScrollView toolboxViewContainer;
        private ListView namespaceListView;
        private ToolbarSearchField toolbarSearchField;
        private ToolbarBreadcrumbs toolbarBreadcrumbs;
        string filterText;
        private hBehaviour chosenHBehaviour;
        private GraphData graphData;
        public static GraphEditorWindow window;
        public static CustomGraphView CurrentGraphView => window.graphView;
        private Dictionary<MethodInfo, CustomGraphView> functionGraphView = new Dictionary<MethodInfo, CustomGraphView>();
        public static void OpenGraphEditorWindow(hBehaviour hBehaviour)
        {
            
            window = GetWindow<GraphEditorWindow>("Graph Editor"); 
            window.minSize = new Vector2(800, 600);
            window.Initialize(hBehaviour);
        }

        private void Initialize(hBehaviour hBehaviour)
        {
            if (this.graphViewTreeAsset == null)
                return;
            
            this.chosenHBehaviour = hBehaviour;
            VisualElement root = rootVisualElement;
            graphViewTreeAsset.CloneTree(root);

            // this.scriptField = root.Q<ObjectField>("_scriptField");
            this.loadButton = root.Q<Button>("_loadButton");
            this.graphViewContainer = root.Q<VisualElement>("_graphField");
            this.toolboxViewContainer = root.Q<ScrollView>("_toolBoxContainer");
            this.toolboxVisualElement = root.Q<VisualElement>("_toolBox");
            this.mainLayout = root.Q<VisualElement>("_mainLayout");
            this.classNameLabel = root.Q<Label>("_classNameLabel");
            this.namespaceListView = root.Q<ListView>("_namespaceListView");
            this.toolbarSearchField = root.Q<ToolbarSearchField>("_toolbarSearchField");
            this.toolbarBreadcrumbs = root.Q<ToolbarBreadcrumbs>("_toolbarBreadCrumbs");

            TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
            splitView.Add(toolboxVisualElement);
            splitView.Add(graphViewContainer);
            mainLayout.Add(splitView);
            splitView.StretchToParentSize();

            this.toolbarSearchField.RegisterValueChangedCallback(OnToolbarSearchFieldChanged);
            ConstructGraph(hBehaviour);


            // * Label
            string initalClassName = $"<b>Class: <color=green>{this.graphData.Name}</color></b>";
            this.toolbarBreadcrumbs.PushItem(initalClassName);

            ReadContent();
        }
        private void ReadContent()
        {

            List<string> namespaces = this.graphData.Namespaces;

            namespaceListView.headerTitle = "Namespaces";
            namespaceListView.itemsSource = namespaces;
            namespaceListView.makeItem = () =>
            {
                TextField textField = new TextField();
                textField.isReadOnly = false;
                return textField;
            };
            namespaceListView.bindItem = (element, i) =>
            {
                (element as TextField).value = namespaces[i];
            };
            namespaceListView.Rebuild();

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
            Foldout propertiesFoldout = new Foldout
            {
                style =
                    {
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
            List<FieldInfo> fieldInfos = this.graphData.Variables.Filter(this.filterText);
            List<PropertyInfo> propertyInfos = this.graphData.Properties.Filter(this.filterText);
            List<MethodInfo> methodInfos = this.graphData.Functions.Filter(this.filterText);

            Dictionary<string, Foldout> fieldNamespaceFoldouts = GroupFieldsByNamespaces(fieldInfos);
            Dictionary<string, Foldout> propertyNamespaceFoldouts = GroupPropertiesByNamespaces(propertyInfos);
            Dictionary<string, Foldout> methodNamespaceFoldouts = GroupMethodsByNamespaces(methodInfos);

            // Add namespace foldouts to main foldouts
            int fieldCount = fieldNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            variableFoldout.text = $"<b>Variables ({fieldCount})</b>";
            foreach (var foldout in fieldNamespaceFoldouts.Values)
            {
                variableFoldout.Add(foldout);
            }

            int propertyCount = propertyNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            propertiesFoldout.text = $"<b>Properties ({propertyCount})</b>";
            foreach (var foldout in propertyNamespaceFoldouts.Values)
            {
                propertiesFoldout.Add(foldout);
            }

            int methodCount = methodNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            functionFoldout.text = $"<b>Functions ({methodCount})</b>";
            foreach (var foldout in methodNamespaceFoldouts.Values)
            {
                functionFoldout.Add(foldout);
            }

            // Clear the toolbox container and add the main foldouts
            toolboxViewContainer.Add(variableFoldout);
            toolboxViewContainer.Add(propertiesFoldout);
            toolboxViewContainer.Add(functionFoldout);
        }
        private void OnToolbarSearchFieldChanged(ChangeEvent<string> evt)
        {
            this.filterText = evt.newValue;
            ReadContent();
        }

        private void ConstructGraph(hBehaviour behaviour)
        {
            this.graphData = new GraphData(behaviour);
            this.graphView = new CustomGraphView(this.graphData)
            {
                name = "Graph View"
            };

            this.graphViewContainer.Add(this.graphView);
            this.graphView.StretchToParentSize();
        }

        Dictionary<string, Foldout> GroupFieldsByNamespaces(List<FieldInfo> fieldInfos)
        {
            Dictionary<string, Foldout> fieldNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (FieldInfo field in fieldInfos)
            {
                string namespaceName = field.DeclaringType.Namespace ?? "Global Namespace";
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
        Dictionary<string, Foldout> GroupPropertiesByNamespaces(List<PropertyInfo> properties)
        {
            Dictionary<string, Foldout> propertyNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (PropertyInfo property in properties)
            {
                string namespaceName = property.DeclaringType.Namespace ?? "Global Namespace";
                if (!propertyNamespaceFoldouts.ContainsKey(namespaceName))
                {
                    propertyNamespaceFoldouts[namespaceName] = new Foldout() 
                    { 
                        text = $"<b>{namespaceName}</b>",    
                        value = false 
                    };
                }

                var icon = EditorGUIUtility.IconContent("d_LODGroup Icon").image;
                string displayName = ObjectNames.NicifyVariableName(property.Name);
                Button propertyButton = Common.CreateButtonWithIcon(icon, new Button() { text = displayName });
                propertyNamespaceFoldouts[namespaceName].Add(propertyButton);
            }
            return propertyNamespaceFoldouts;
        }
        Dictionary<string, Foldout> GroupMethodsByNamespaces(List<MethodInfo> methods)
        {
            Dictionary<string, Foldout> methodNamespaceFoldouts = new Dictionary<string, Foldout>();
            foreach (MethodInfo method in methods)
            {
                string namespaceName = method.DeclaringType.Namespace ?? "Global Namespace";
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
                        eNodeType nodeType = eNodeType.Function;
                        string nodeName = method.Name;
                        Type nodeReturnType = method.ReturnType;
                        GraphEvents.ON_TOOLBOX_ITEM_CLICKED?.Invoke(nodeType, nodeName, nodeReturnType);
                    }
                ) { text = method.Name });

                methodNamespaceFoldouts[namespaceName].Add(methodButton);
            }
            return methodNamespaceFoldouts;
        }

        private object SimplifyTypeName(Type fieldType)
        {
            if (fieldType.IsGenericType)
            {
                string typeName = fieldType.Name;
                int index = typeName.IndexOf('`');
                if (index > 0)
                {
                    typeName = typeName.Substring(0, index);
                }
                if (fieldType.GenericTypeArguments.Length > 0)
                    return $"{typeName}<{fieldType.GenericTypeArguments[0].Name}>";
            }
            return fieldType.Name;
        }
    }
}