using System;
using System.Collections.Generic;
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
    private VisualElement graphViewContainer;
    private ScrollView toolboxViewContainer;

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


        this.loadButton.clicked += OnLoadButtonClicked;
        ConstructGraph();

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

    private void OnLoadButtonClicked()
    {
        MonoScript monoScript = scriptField.value as MonoScript;
        if (monoScript != null)
        {
            Type scriptType = monoScript.GetClass();
            if (scriptType != null)
            {
                Debug.Log($"Class: {scriptType.Name}");

                // Create main foldouts for fields, properties, and methods
                Foldout fieldsFoldout = new Foldout() { text = "Fields" };
                Foldout propertiesFoldout = new Foldout() { text = "Properties" };
                Foldout methodsFoldout = new Foldout() { text = "Methods" };
                
                // Enable rich text for main foldouts
                fieldsFoldout.Q<Label>().enableRichText = true;
                propertiesFoldout.Q<Label>().enableRichText = true;
                methodsFoldout.Q<Label>().enableRichText = true;

                // Dictionaries to store namespace foldouts
                Dictionary<string, Foldout> fieldNamespaceFoldouts = new Dictionary<string, Foldout>();
                Dictionary<string, Foldout> propertyNamespaceFoldouts = new Dictionary<string, Foldout>();
                Dictionary<string, Foldout> methodNamespaceFoldouts = new Dictionary<string, Foldout>();

                // Get and add fields
                FieldInfo[] fields = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (FieldInfo field in fields)
                {
                    string namespaceName = field.DeclaringType.Namespace ?? "No Namespace";
                    if (!fieldNamespaceFoldouts.ContainsKey(namespaceName))
                    {
                        fieldNamespaceFoldouts[namespaceName] = new Foldout() { text = namespaceName };
                    }

                    string text = $"<color=yellow>({field.FieldType})</color> {field.Name}";
                    Label fieldLabel = new Label(text);
                    fieldLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
                    fieldLabel.enableRichText = true;
                    fieldNamespaceFoldouts[namespaceName].Add(fieldLabel);
                }

                // Get and add properties
                PropertyInfo[] properties = scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (PropertyInfo property in properties)
                {
                    string namespaceName = property.DeclaringType.Namespace ?? "No Namespace";
                    if (!propertyNamespaceFoldouts.ContainsKey(namespaceName))
                    {
                        propertyNamespaceFoldouts[namespaceName] = new Foldout() { text = namespaceName };
                    }

                    string text = $"<color=yellow>({property.PropertyType})</color> {property.Name}";
                    Label propertyLabel = new Label(text);
                    propertyLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
                    propertyLabel.enableRichText = true;
                    propertyNamespaceFoldouts[namespaceName].Add(propertyLabel);
                }

                // Get and add methods
                MethodInfo[] methods = scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (MethodInfo method in methods)
                {
                    string namespaceName = method.DeclaringType.Namespace ?? "No Namespace";
                    if (!methodNamespaceFoldouts.ContainsKey(namespaceName))
                    {
                        methodNamespaceFoldouts[namespaceName] = new Foldout() { text = namespaceName };
                    }

                    string text = $"<color=yellow>({method.ReturnType})</color> {method.Name}";
                    Label methodLabel = new Label(text);
                    methodLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
                    methodLabel.enableRichText = true;
                    methodNamespaceFoldouts[namespaceName].Add(methodLabel);
                }

                // Add namespace foldouts to main foldouts
                foreach (var foldout in fieldNamespaceFoldouts.Values)
                {
                    fieldsFoldout.Add(foldout);
                }
                foreach (var foldout in propertyNamespaceFoldouts.Values)
                {
                    propertiesFoldout.Add(foldout);
                }
                foreach (var foldout in methodNamespaceFoldouts.Values)
                {
                    methodsFoldout.Add(foldout);
                }

                // Clear the toolbox container and add the main foldouts
                toolboxViewContainer.Clear();
                toolboxViewContainer.Add(fieldsFoldout);
                toolboxViewContainer.Add(propertiesFoldout);
                toolboxViewContainer.Add(methodsFoldout);
            }
            else
            {
                Debug.LogError("Selected script does not have a valid class.");
            }
        }
        else
        {
            Debug.LogError("No script selected.");
        }
    }
}