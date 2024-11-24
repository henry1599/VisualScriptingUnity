using System;
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

                // Create foldouts for fields, properties, and methods
                Foldout fieldsFoldout = new Foldout() { text = "Fields" };
                Foldout propertiesFoldout = new Foldout() { text = "Properties" };
                Foldout methodsFoldout = new Foldout() { text = "Methods" };

                // Get and add fields
                FieldInfo[] fields = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (FieldInfo field in fields)
                {
                    Button fieldButton = new Button(() => Debug.Log($"Field: {field.Name} ({field.FieldType})"))
                    {
                        text = $"{field.Name}"// ({field.FieldType})"
                    };
                    fieldButton.style.unityTextAlign = TextAnchor.MiddleLeft;
                    fieldButton.enableRichText = true;
                    fieldsFoldout.Add(fieldButton);
                }

                // Get and add properties
                PropertyInfo[] properties = scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (PropertyInfo property in properties)
                {
                    Button propertyButton = new Button(() => Debug.Log($"Property: {property.Name} ({property.PropertyType})"))
                    {
                        text = $"{property.Name}"// ({property.PropertyType})"
                    };
                    propertyButton.style.unityTextAlign = TextAnchor.MiddleLeft;
                    propertyButton.enableRichText = true;
                    propertiesFoldout.Add(propertyButton);
                }

                // Get and add methods
                MethodInfo[] methods = scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (MethodInfo method in methods)
                {
                    Button methodButton = new Button(() => Debug.Log($"Method: {method.Name} (Return Type: {method.ReturnType})"))
                    {
                        text = $"{method.Name} (Return Type: {method.ReturnType})"
                    };
                    methodButton.style.unityTextAlign = TextAnchor.MiddleLeft;
                    methodButton.enableRichText = true;
                    methodsFoldout.Add(methodButton);
                }

                // Clear the toolbox container and add the foldouts
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