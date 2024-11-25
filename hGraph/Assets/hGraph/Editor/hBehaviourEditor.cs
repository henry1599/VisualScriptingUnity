using UnityEditor;
using UnityEngine;
using hGraph.Editor;

[CustomEditor(typeof(MonoScript))]
public class hBehaviourScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MonoScript monoScript = (MonoScript)target;

        System.Type scriptClass = monoScript.GetClass();
        if (scriptClass != null && typeof(hBehaviour).IsAssignableFrom(scriptClass))
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Node Editor"))
            {
                hBehaviour instance = (hBehaviour)System.Activator.CreateInstance(scriptClass);
                GraphEditorWindow.OpenGraphEditorWindow(instance);
            }
        }
        else
        {
            base.OnInspectorGUI();
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
            GraphEditorWindow.OpenGraphEditorWindow((hBehaviour)target);
        }
    }
}
