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
        RemoveAllNodes();
        this.ParsedScript = null;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public override float ZoomMinScale => 0.05f;
    public void BuildGraph(ParsedObject item)
    {
        RemoveAllNodes();
        switch (item.Category)
        {
            case eParsedDataType.Class:
                BuildGraphFromClass(item.Name);
                break;
            case eParsedDataType.Method:
                BuildGraphFromMethod(item.Name);
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
        // * Default
        // * A method node has to have an entry and an exit
        Node entryNode = new hNode()
        {
            Name = "Entry",
            Position = new Vector2(100, 100),
            HasEntry = false
        };
        entryNode.AddPort(new Port()
        {
            Name = "exit",
            Direction = PortDirection.Output,
            Type = typeof(hNode)
        });
        Node exitNode = new hNode()
        {
            Name = "Exit",
            Position = new Vector2(250, 100),
            HasExit = false
        };
        exitNode.AddPort(new Port()
        {
            Name = "entry",
            Direction = PortDirection.Input,
            Type = typeof(hNode)
        });
        Node exampleNode = new hNode()
        {
            Name = "Example",
            Position = new Vector2(400, 100),
        };
        exampleNode.AddPort(new Port()
        {
            Name = "entry",
            Direction = PortDirection.Input,
            Type = typeof(hNode)
        });
        exampleNode.AddPort(new Port()
        {
            Name = "exit",
            Direction = PortDirection.Output,
            Type = typeof(hNode)
        });

        AddNode(entryNode);
        AddNode(exitNode);
        AddNode(exampleNode);

        AddEdge(entryNode.GetPort("exit"), exitNode.GetPort("entry"));


        // TODO: Read content of method and create corresponding nodes
        // ParsedMethod method = ParsedScript.Classes[className].Methods[0];
        
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
