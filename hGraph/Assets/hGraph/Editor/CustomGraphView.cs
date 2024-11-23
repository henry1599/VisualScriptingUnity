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
        grid.StretchToParentSize();

        // Add manipulators for zooming and panning
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        // Set up the style
        styleSheets.Add(Resources.Load<StyleSheet>("GraphViewStyle"));

        // Handle right-click context menu
        this.AddManipulator(new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("Add Node/Start", action => AddNode(evt.localMousePosition, "Start"));
            evt.menu.AppendAction("Add Node/Update", action => AddNode(evt.localMousePosition, "Update"));
            evt.menu.AppendAction("Add Node/Function", action => AddNode(evt.localMousePosition, "Function"));
        }));
    }

    private void AddNode(Vector2 position, string nodeType)
    {
        Node node = new Node(position)
        {
            title = $"{nodeType} Node"
        };
        node.SetPosition(node.rect);
        AddElement(node);
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.Where(endPort =>
                    endPort.direction != startPort.direction &&
                    endPort.node != startPort.node &&
                    endPort.portType == startPort.portType).ToList();
    }
}