using System;
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
    public Type Type;
    public EntryPort Entry;
    public FinishPort Finish;
    public List<NodePortBase> InputPorts;
    public NodePortBase OutputPort;
    public NodeDataBase NodeFrom;
    public NodeDataBase NodeTo;
    public NodeDataBase(bool hasEntryPort = true)
    {
        Entry = hasEntryPort ? new EntryPort() : null;
        Finish = new FinishPort();
        InputPorts = new List<NodePortBase>();
        OutputPort = null;
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