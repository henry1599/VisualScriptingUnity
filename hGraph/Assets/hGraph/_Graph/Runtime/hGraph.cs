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
    [Button]
    public void ClearGraph()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "Clear Graph");
#endif
        // Clear the nodes and edges in the graph
        RemoveAllNodes();

        // Reset other properties if needed
        this.ParsedScript = null;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public void BuildGraph(eParsedDataType perspective, string name)
    {
        switch (perspective)
        {
            case eParsedDataType.Class:
                BuildGraphFromClass(name);
                break;
            case eParsedDataType.Method:
                BuildGraphFromMethod(name);
                break;
        }
    }
    void BuildGraphFromClass(string className)
    {
        ParsedMethod startMethod = ParsedScript.Classes[className].Methods.Find(m => m.Name == "Start" && m.Params.Count == 0 && m.ReturnType == "void");
        ParsedMethod updateMethod = ParsedScript.Classes[className].Methods.Find(m => m.Name == "Update" && m.Params.Count == 0 && m.ReturnType == "void");
        hNode startNode = new hNode();
        hNode updateNode = new hNode();
        if (startMethod != null)
        {
            startNode = new hNode()
            {
                Name = "Start",
                Position = new Vector2(100, 100),
                HasEntry = false
            };
            startNode.AddPort(new Port()
            {
                Name = "exit",
                Direction = PortDirection.Output,
                Type = typeof(hNode)
            });
            AddNode(startNode);
        }
        if (updateMethod != null)
        {
            updateNode = new hNode()
            {
                Name = "Update",
                Position = new Vector2(250, 100)
            };
            updateNode.AddPort(new Port()
            {
                Name = "entry",
                Direction = PortDirection.Input,
                Type = typeof(hNode)
            });
            updateNode.AddPort(new Port()
            {
                Name = "exit",
                Direction = PortDirection.Output,
                Type = typeof(hNode)
            });
            AddNode(updateNode);
        }
        if (startMethod != null && updateMethod != null)
        {
            AddEdge(startNode.GetPort("exit"), updateNode.GetPort("entry"));
        }
    }
    void BuildGraphFromMethod(string className)
    {

    }
#if UNITY_EDITOR
    [Button]
    public void Parse()
    {
        ParsedScript = ParsedScript.Create(AssetDatabase.GetAssetPath(Script));
    }
    [Button]
    public void OpenInEditor()
    {
#if UNITY_EDITOR
        EditorApplication.delayCall += () =>
        {
            var editorUtilityType = System.Type.GetType("EditorGraphOpenner, BlueGraph.Editor");
            if (editorUtilityType != null)
            {
                var method = editorUtilityType.GetMethod("OpenEditGraph", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                if (method != null)
                {
                    method.Invoke(null, new object[] { this });
                }
                else
                {
                    Debug.LogError("Method OpenEditGraph not found in GraphEditorUtility.");
                }
            }
            else
            {
                Debug.LogError("Type EditorGraphOpenner not found.");
            }
        };
#endif
    }
#endif
}
