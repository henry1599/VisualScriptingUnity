using System.Collections.Generic;
using log4net.Util;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomNode : Node
{
    private Port entryPort;
    private Port finishPort;
    private List<Port> inputPorts;
    private List<Port> outputPorts;
    private Vector2 size;
    private VisualElement entryContainer;
    private VisualElement finishContainer;
    private Color nodeBackgroundColor = new Color(90f / 255f, 90f / 255f, 90f / 255f, 1f);

    public CustomNode(string nodeName)
    {
        title = nodeName;
        SetupNodeStyle();
        
        this.mainContainer.style.height = this.size.y;
        
        // Ports
        CreateSidePorts();
        CreateEntryPort();
        CreateFinishPort();

        RefreshExpandedState();
        RefreshPorts();
    }

    private void CreateEntryPort()
    {
        this.entryContainer = new VisualElement()
        {
            style = 
            { 
                flexShrink = 1,
                flexGrow = 0 
            }
        };
        this.entryPort = Port.Create<Edge>(Orientation.Vertical, Direction.Input, Port.Capacity.Single, this.GetType());
        
        // * Setting port
        this.entryPort.portName = string.Empty;
        this.entryPort.style.flexDirection = FlexDirection.Column;

        Label portLabel = this.entryPort.contentContainer.Q<Label>("type");
        portLabel.style.height = 0;

        VisualElement mainPortVS = this.entryPort.contentContainer.Q<VisualElement>("connector");
        VisualElement capVS = mainPortVS.Q<VisualElement>("cap");

        mainPortVS.style.borderBottomWidth = 2;
        mainPortVS.style.borderTopWidth = 2;
        mainPortVS.style.borderLeftWidth = 2;
        mainPortVS.style.borderRightWidth = 2;
        mainPortVS.style.height = mainPortVS.style.width = 18;

        capVS.style.width = capVS.style.height = 10;


        this.entryContainer.Add(this.entryPort);
        this.mainContainer.Insert(0, entryContainer);
    }
    private void CreateFinishPort()
    {
        this.finishContainer = new VisualElement()
        {
            style = 
            { 
                flexShrink = 1,
                flexGrow = 0 
            }
        };
        this.finishPort = Port.Create<Edge>(Orientation.Vertical, Direction.Output, Port.Capacity.Single, this.GetType());
        
        // * Setting port
        this.finishPort.portName = string.Empty;
        this.finishPort.style.flexDirection = FlexDirection.Column;

        Label portLabel = this.finishPort.contentContainer.Q<Label>("type");
        portLabel.style.height = 0;

        VisualElement mainPortVS = this.finishPort.contentContainer.Q<VisualElement>("connector");
        VisualElement capVS = mainPortVS.Q<VisualElement>("cap");

        mainPortVS.style.borderBottomWidth = 2;
        mainPortVS.style.borderTopWidth = 2;
        mainPortVS.style.borderLeftWidth = 2;
        mainPortVS.style.borderRightWidth = 2;
        mainPortVS.style.height = mainPortVS.style.width = 18;

        capVS.style.width = capVS.style.height = 10;


        this.finishContainer.Add(this.finishPort);
        this.mainContainer.Add(finishContainer);
    }

    private void CreateSidePorts()
    {
        // Example input on the left
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, this.GetType());
        inputPort.portName = "Input";
        inputContainer.Add(inputPort);

        // Example output on the right
        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, this.GetType());
        outputPort.portName = "Output";
        outputContainer.Add(outputPort);
    }

    private void SetupNodeStyle()
    {
        this.size = new Vector2(200, 150);
        style.width = this.size.x;  // Node width
        style.height = this.size.y; // Node height

        this.style.backgroundColor = this.nodeBackgroundColor;
        this.style.borderBottomLeftRadius = 
        this.style.borderBottomRightRadius =
        this.style.borderTopLeftRadius =
        this.style.borderTopRightRadius = 10;


        var nodeBorder = this.Q<VisualElement>("node-border");
        nodeBorder.style.borderBottomColor = 
        nodeBorder.style.borderTopColor =
        nodeBorder.style.borderLeftColor =
        nodeBorder.style.borderRightColor = new Color(0.5f, 0.5f, 0.5f, 0);
    }
}
