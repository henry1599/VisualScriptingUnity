using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : UnityEditor.Experimental.GraphView.Node
{
    public Node()
    {
        title = "Node";
        SetPosition(new Rect(100, 100, 200, 150));

        // Add input port
        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
        inputPort.portName = "Input";
        inputContainer.Add(inputPort);

        // Add output port
        Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
        outputPort.portName = "Output";
        outputContainer.Add(outputPort);

        RefreshExpandedState();
        RefreshPorts();
    }
}