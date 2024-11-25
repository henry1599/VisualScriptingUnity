using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : UnityEditor.Experimental.GraphView.Node
{
    public Rect rect { get; private set; }
    public Port EntryPort;
    public Port FinishPort;
    public List<Port> InputPorts;
    public List<Port> OutputPorts;
    private VisualElement entryPortContainer;
    private VisualElement finishPortContainer;
    public Node(Vector2 position)
    {
        this.InputPorts = new List<Port>();
        this.OutputPorts = new List<Port>();

        title = "Node";
        this.rect = new Rect(position.x, position.y, 200, 150);
        SetPosition(this.rect);

        
        // AddFinishPort();

        Refresh();
    }
    public virtual void Refresh()
    {
        RefreshExpandedState();
        RefreshPorts();
    }
    public virtual void AddInputPort(string name, Orientation orientation = default, Port.Capacity capacity = default)
    {
        var port = InstantiatePort(orientation, Direction.Input, capacity, this.GetType());
        port.portName = name;
        this.inputContainer.Add(port);
    }
    public virtual void AddOutputPort(string name, Orientation orientation = default, Port.Capacity capacity = default)
    {
        var port = InstantiatePort(orientation, Direction.Output, capacity, this.GetType());
        port.portName = name;
        this.outputContainer.Add(port);
    }
}