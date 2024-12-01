using BlueGraph;
using UnityEngine;
using Sirenix.OdinInspector;



#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Graph", menuName = "Graph")]
public class hCustomGraph : Graph
{
    public Object Script;
    public ParsedScript ParsedScript;
#if UNITY_EDITOR
    [Button]
    public void Parse()
    {
        ParsedScript = ParsedScript.Create(AssetDatabase.GetAssetPath(Script));
    }
#endif
}
