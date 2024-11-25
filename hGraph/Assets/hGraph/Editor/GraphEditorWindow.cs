using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codice.Client.BaseCommands;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphEditorWindow : EditorWindow
{
    public VisualTreeAsset graphViewTreeAsset;
    private CustomGraphView graphView;
    private ObjectField scriptField;
    private Button loadButton;

    private VisualElement mainLayout;
    private Label classNameLabel;
    private VisualElement graphViewContainer;
    private VisualElement toolboxVisualElement;
    private ScrollView toolboxViewContainer;
    private ListView namespaceListView;
    private ToolbarSearchField toolbarSearchField;
    string filterText;

    [MenuItem("Tools/Graph %g")]
    public static void OpenGraphEditorWindow()
    {
        GraphEditorWindow window = GetWindow<GraphEditorWindow>("Graph Editor");
        window.minSize = new Vector2(800, 600);
        window.Initialize();
    }

    private void Initialize()
    {
        if (this.graphViewTreeAsset == null)
            return;

        VisualElement root = rootVisualElement;
        graphViewTreeAsset.CloneTree(root);

        this.scriptField = root.Q<ObjectField>("_scriptField");
        this.loadButton = root.Q<Button>("_loadButton");
        this.graphViewContainer = root.Q<VisualElement>("_graphField");
        this.toolboxViewContainer = root.Q<ScrollView>("_toolBoxContainer");
        this.toolboxVisualElement = root.Q<VisualElement>("_toolBox");
        this.mainLayout = root.Q<VisualElement>("_mainLayout");
        this.classNameLabel = root.Q<Label>("_classNameLabel");
        this.namespaceListView = root.Q<ListView>("_namespaceListView");
        this.toolbarSearchField = root.Q<ToolbarSearchField>("_toolbarSearchField");

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        splitView.Add(toolboxVisualElement);
        splitView.Add(graphViewContainer);
        mainLayout.Add(splitView);
        splitView.StretchToParentSize();

        this.loadButton.clicked += OnLoadButtonClicked;
        this.toolbarSearchField.RegisterValueChangedCallback(OnToolbarSearchFieldChanged);
        ConstructGraph();
    }

    private void OnToolbarSearchFieldChanged(ChangeEvent<string> evt)
    {
        this.filterText = evt.newValue;
        ReloadContent(scriptField.value as MonoScript);
    }

    private void ConstructGraph()
    {
        this.graphView = new CustomGraphView()
        {
            name = "Graph View"
        };

        this.graphViewContainer.Add(this.graphView);
        this.graphView.StretchToParentSize();
    }

    void ReloadContent(MonoScript monoScript)
    {
        this.toolboxViewContainer.Clear();
        Type scriptType = monoScript.GetClass();

        // * Label
        this.classNameLabel.text = $"<b>Class: <color=green>{scriptType.Name}</color></b>";



        // * Namespaces
        var namespaces = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select(f => f.DeclaringType.Namespace)
            .Concat(scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select(p => p.DeclaringType.Namespace))
            .Concat(scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select(m => m.DeclaringType.Namespace))
            .Where(ns => !string.IsNullOrEmpty(ns))
            .Distinct()
            .ToList();

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



        if (scriptType != null)
        {
            Debug.Log($"Class: {scriptType.Name}");
            // Create main foldouts for fields, properties, and methods
            Foldout fieldsFoldout = new Foldout
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
            Foldout methodsFoldout = new Foldout
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
            List<FieldInfo> fieldInfos = scriptType.GetFieldList(namespaces).Filter(this.filterText);
            List<PropertyInfo> propertyInfos = scriptType.GetPropertyList(namespaces).Filter(this.filterText);
            List<MethodInfo> methodInfos = scriptType.GetMethodList(namespaces).Filter(this.filterText);

            Dictionary<string, Foldout> fieldNamespaceFoldouts = GroupFieldsByNamespaces(fieldInfos);
            Dictionary<string, Foldout> propertyNamespaceFoldouts = GroupPropertiesByNamespaces(propertyInfos);
            Dictionary<string, Foldout> methodNamespaceFoldouts = GroupMethodsByNamespaces(methodInfos);

            // Add namespace foldouts to main foldouts
            int fieldCount = fieldNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            fieldsFoldout.text = $"<b>Fields ({fieldCount})</b>";
            foreach (var foldout in fieldNamespaceFoldouts.Values)
            {
                fieldsFoldout.Add(foldout);
            }

            int propertyCount = propertyNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            propertiesFoldout.text = $"<b>Properties ({propertyCount})</b>";
            foreach (var foldout in propertyNamespaceFoldouts.Values)
            {
                propertiesFoldout.Add(foldout);
            }

            int methodCount = methodNamespaceFoldouts.Values.Select(child => child.childCount).Sum();
            methodsFoldout.text = $"<b>Methods ({methodCount})</b>";
            foreach (var foldout in methodNamespaceFoldouts.Values)
            {
                methodsFoldout.Add(foldout);
            }

            // Clear the toolbox container and add the main foldouts
            toolboxViewContainer.Add(fieldsFoldout);
            toolboxViewContainer.Add(propertiesFoldout);
            toolboxViewContainer.Add(methodsFoldout);
        }
        else
        {
            Debug.LogError("Selected script does not have a valid class.");
        }
    }
    private void OnLoadButtonClicked()
    {
        MonoScript monoScript = scriptField.value as MonoScript;
        if (monoScript != null)
        {
            ReloadContent(monoScript);
        }
        else
        {
            Debug.LogError("No script selected.");
        }
    }
    Dictionary<string, Foldout> GroupFieldsByNamespaces(List<FieldInfo> fieldInfos)
    {
        Dictionary<string, Foldout> fieldNamespaceFoldouts = new Dictionary<string, Foldout>();
        foreach (FieldInfo field in fieldInfos)
        {
            string namespaceName = field.DeclaringType.Namespace ?? "No Namespace";
            if (!fieldNamespaceFoldouts.ContainsKey(namespaceName))
            {
                fieldNamespaceFoldouts[namespaceName] = new Foldout() { text = $"<b>{namespaceName}</b>" };
            }

            bool isInstance = field.DeclaringType == scriptField.value.GetType();
            
            string text = string.Empty;
            if (isInstance)
                text = $"<b><color=yellow>(Instance)</color> {field.Name}</b>";
            else
                text = $"<b><color=yellow>({SimplifyTypeName(field.FieldType)})</color> {field.Name}</b>";
            Label fieldLabel = new Label(text);
            fieldLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
            fieldLabel.enableRichText = true;
            fieldNamespaceFoldouts[namespaceName].Add(fieldLabel);
        }
        return fieldNamespaceFoldouts;
    }
    Dictionary<string, Foldout> GroupPropertiesByNamespaces(List<PropertyInfo> properties)
    {
        Dictionary<string, Foldout> propertyNamespaceFoldouts = new Dictionary<string, Foldout>();
        foreach (PropertyInfo property in properties)
        {
            string namespaceName = property.DeclaringType.Namespace ?? "No Namespace";
            if (!propertyNamespaceFoldouts.ContainsKey(namespaceName))
            {
                propertyNamespaceFoldouts[namespaceName] = new Foldout() { text = $"<b>{namespaceName}</b>" };
            }

            string text = $"<b><color=yellow>({SimplifyTypeName(property.PropertyType)})</color> {property.Name}</b>";
            Label propertyLabel = new Label(text);
            propertyLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
            propertyLabel.enableRichText = true;
            propertyNamespaceFoldouts[namespaceName].Add(propertyLabel);
        }
        return propertyNamespaceFoldouts;
    }
    Dictionary<string, Foldout> GroupMethodsByNamespaces(List<MethodInfo> methods)
    {
        Dictionary<string, Foldout> methodNamespaceFoldouts = new Dictionary<string, Foldout>();
        foreach (MethodInfo method in methods)
        {
            string namespaceName = method.DeclaringType.Namespace ?? "No Namespace";
            if (!methodNamespaceFoldouts.ContainsKey(namespaceName))
            {
                methodNamespaceFoldouts[namespaceName] = new Foldout() { text = $"<b>{namespaceName}</b>" };
            }

            string text = $"<b><color=yellow>({SimplifyTypeName(method.ReturnType)})</color> {method.Name}</b>";
            Label methodLabel = new Label(text);
            methodLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
            methodLabel.enableRichText = true;
            methodNamespaceFoldouts[namespaceName].Add(methodLabel);
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