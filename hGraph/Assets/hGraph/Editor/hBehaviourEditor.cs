using BlueGraph;
using BlueGraph.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(hCustomGraph))]
public class hBehaviourScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        if (GUILayout.Button("Open Node Editor"))
        {
            GraphEditorWindow.OpenGraphEditorWindow(target as hCustomGraph);
        }
    }
}

[CustomEditor(typeof(hBehaviour))]
public class hBehaviourComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Open Node Editor"))
        {
            GraphEditorWindow.OpenGraphEditorWindow(target as hCustomGraph);
        }
    }
}
