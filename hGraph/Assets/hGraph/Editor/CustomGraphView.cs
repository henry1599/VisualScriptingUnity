using System;
using System.Collections.Generic;
using System.Linq;
using hGraph.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomGraphView : GraphView
{
    private GraphData data;
    public CustomGraphView(GraphData graphData)
    {
        data = graphData;
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

        this.data.OnNewNodeAddedToGraph += OnNewNodeAddedToGraph;
    }

    private void OnNewNodeAddedToGraph(NodeDataBase node)
    {
        // TODO
        CustomNode customNode = new CustomNode(node);
        CustomGraphView currentGraphView = GraphEditorWindow.CurrentGraphView;
        var centerPosition = currentGraphView.contentViewContainer.WorldToLocal(currentGraphView.layout.center);
        customNode.SetPosition(new Rect(centerPosition, Vector2.zero));
        AddElement(customNode);
    }

    private void OnGUI() 
    {
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