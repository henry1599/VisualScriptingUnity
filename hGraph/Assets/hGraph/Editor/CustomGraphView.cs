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
            evt.menu.AppendAction("Add Node", action => AddNode(evt.mousePosition));
        }));
    }

    private void AddNode(Vector2 position)
    {
        Node node = new Node
        {
            title = "New Node"
        };
        node.SetPosition(new Rect(position, new Vector2(200, 150)));
        AddElement(node);
    }
}