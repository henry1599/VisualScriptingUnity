using System.Collections;
using System.Collections.Generic;
using BlueGraph;
using BlueGraph.Editor;
using UnityEditor;
using UnityEngine;

public class EditorGraphOpenner : EditorWindow
{
    private ScriptableObject hCustomGraph;

    [MenuItem("Tools/hGraph/Graph Openner %g")]
    public static void ShowWindow()
    {
        GetWindow<EditorGraphOpenner>("Graph Openner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Graph Openner", EditorStyles.boldLabel);

        hCustomGraph = (ScriptableObject)EditorGUILayout.ObjectField("Custom Graph", hCustomGraph, typeof(ScriptableObject), false);

        if (GUILayout.Button("Load Graph"))
        {
            LoadGraph();
        }
    }

    private void LoadGraph()
    {
        if (hCustomGraph != null)
        {
            GraphEditorWindow.OpenGraphEditorWindow((hCustomGraph)hCustomGraph);
        }
    }
}