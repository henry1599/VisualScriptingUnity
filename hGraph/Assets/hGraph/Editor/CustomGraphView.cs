using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomGraphView : GraphView
{
    public CustomGraphView()
    {
        // Add a grid background
        GridBackground grid = new GridBackground();
        Insert(0, grid);

        // Add manipulators for zooming and panning
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        // Set up the style
        this.styleSheets.Add(Resources.Load<StyleSheet>("GraphViewStyle"));

        // Handle right-click context menu
        this.AddManipulator(new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("Add Node/Start", action => AddNode(evt.localMousePosition, "Start"));
            evt.menu.AppendAction("Add Node/Update", action => AddNode(evt.localMousePosition, "Update"));
            evt.menu.AppendAction("Add Node/Function", action => AddNode(evt.localMousePosition, "Function"));
        }));
    }
    private void OnGUI() {
        SetEdgeWidth(10);
    }

    private void AddNode(Vector2 position, string nodeType)
    {
        CustomNode node = new CustomNode($"{nodeType} Node");
        node.SetPosition(new Rect(position, Vector2.zero));
        // node.AddInputPort("Param");
        // node.AddOutputPort("Output");
        // node.Refresh();
        AddElement(node);
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.Where(endPort =>
                    endPort.direction != startPort.direction &&
                    endPort.node != startPort.node &&
                    endPort.portType == startPort.portType).ToList();
    }
    public void SetEdgeWidth(float width)
    {
        foreach (var edge in this.edges.ToList())
        {
            edge.style.width = width;
        }
    }
}