using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : UnityEditor.Experimental.GraphView.Node
{
    public Rect rect { get; private set; }
    public Port inputPort;
    public Port outputPort;
    public Node(Vector2 position)
    {
        title = "Node";
        this.rect = new Rect(position.x, position.y, 200, 150);
        SetPosition(this.rect);

        // Add input port
        this.inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, this.GetType());
        this.inputPort.portName = "Input";
        inputContainer.Add(this.inputPort);

        // Add output port
        this.outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, this.GetType());
        this.outputPort.portName = "Output";
        outputContainer.Add(this.outputPort);


        RefreshExpandedState();
        RefreshPorts();
    }
}