using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eNodeType
{
    Class,
    Variable,
    Property,
    Function,
    Custom,
}
public enum eNodePortFlowType
{
    Entry,
    Finish
}

public class NodeDataBase
{
    public eNodeType NodeType;
    public string Name;
    public string DisplayName;
    public string Info;
    public EntryPort Entry;
    public FinishPort Finish;
    public List<NodePortBase> InputPorts;
    public List<NodePortBase> OutputPorts;
    public NodeDataBase NodeFrom;
    public NodeDataBase NodeTo;
    public NodeDataBase()
    {
        Entry = new EntryPort();
        Finish = new FinishPort();
        InputPorts = new List<NodePortBase>();
        OutputPorts = new List<NodePortBase>();
        NodeFrom = null;
        NodeTo = null;

        Name = "New Node";
        DisplayName = "New Node";
        Info = "New Node Info";
        NodeType = eNodeType.Class;
    }
}
public class NodeConnectionBase
{
    public NodeDataBase NodeFrom;
    public NodeDataBase NodeTo;
    public NodePortBase PortFrom;
    public NodePortBase PortTo;
}

public class NodePortBase
{
    public string Name;
    public System.Type Type;
}
public class EntryPort : NodeFlowPortBase
{
    public override eNodePortFlowType FlowType => eNodePortFlowType.Entry;
}
public class FinishPort : NodeFlowPortBase
{
    public override eNodePortFlowType FlowType => eNodePortFlowType.Finish;
}
public abstract class NodeFlowPortBase
{
    public abstract eNodePortFlowType FlowType { get; }
}