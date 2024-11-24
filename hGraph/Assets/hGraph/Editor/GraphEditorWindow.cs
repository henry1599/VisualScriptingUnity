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
    private VisualElement toolboxViewContainer;

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
        this.toolboxViewContainer = root.Q<VisualElement>("_toolBox");


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

                // Get and print fields
                FieldInfo[] fields = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (FieldInfo field in fields)
                {
                    Debug.Log($"Field: {field.Name} ({field.FieldType})");
                }

                // Get and print properties
                PropertyInfo[] properties = scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (PropertyInfo property in properties)
                {
                    Debug.Log($"Property: {property.Name} ({property.PropertyType})");
                }

                // Get and print methods
                MethodInfo[] methods = scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (MethodInfo method in methods)
                {
                    Debug.Log($"Method: {method.Name} (Return Type: {method.ReturnType})");
                }
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