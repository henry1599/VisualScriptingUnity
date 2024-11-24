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
    private VisualTreeAsset graphViewTreeAsset;
    private CustomGraphView graphView;

    private VisualElement scriptFieldContainer;
    private ObjectField scriptField;
    private Button loadButton;

    [MenuItem("Tools/Graph %g")]
    public static void OpenGraphEditorWindow()
    {
        GraphEditorWindow window = GetWindow<GraphEditorWindow>("Graph Editor");
        window.minSize = new Vector2(800, 600);
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