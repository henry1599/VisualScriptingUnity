using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphData
{
    public List<string> Namespaces;

    public List<NodeDataBase> Nodes;
    public List<NodeConnectionBase> Connections;

    private List<NodeDataBase> nodesInGraph;

    public string Name;
    public List<FieldInfo> Variables;
    public List<PropertyInfo> Properties;
    public List<MethodInfo> Functions;
    hBehaviour behaviour;
    MethodInfo methodInfo;
    public event Action<NodeDataBase> OnNewNodeAddedToGraph;
    public GraphData(hBehaviour behaviour)
    {
        this.behaviour = behaviour;
        Nodes = new List<NodeDataBase>();
        Connections = new List<NodeConnectionBase>();
        this.nodesInGraph = new List<NodeDataBase>();

        ReadBehaviour(behaviour);

        
        GraphEvents.ON_TOOLBOX_ITEM_CLICKED += HandleToolboxItemClicked;
    }
    public GraphData(hBehaviour behaviour, MethodInfo methodInfo)
    {
        this.behaviour = behaviour;
        this.methodInfo = methodInfo;
        Nodes = new List<NodeDataBase>();
        Connections = new List<NodeConnectionBase>();
        this.nodesInGraph = new List<NodeDataBase>();

        ReadBehaviour(behaviour);

        GraphEvents.ON_TOOLBOX_ITEM_CLICKED += HandleToolboxItemClicked;
    }

    private void HandleToolboxItemClicked(eNodeType nodeType, string name, Type type)
    {
        Debug.Log($"Add node Type: {nodeType}, Name: {name}, Type: {type}");
        NodeDataBase candidate = Nodes.Find(n => n.Name == name && n.NodeType == nodeType && n.Type == type);
        if (candidate == null)
            return;
        this.nodesInGraph.Add(candidate);
        OnNewNodeAddedToGraph?.Invoke(candidate);
    }

    private void ReadBehaviour(hBehaviour behaviour)
    {
        Type scriptType = behaviour.GetType();
        // * Name
        Name = scriptType.Name;

        // * Namespaces
        Namespaces = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select(f => f.DeclaringType.Namespace)
            .Concat(scriptType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select(p => p.DeclaringType.Namespace))
            .Concat(scriptType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Select(m => m.DeclaringType.Namespace))
            .Where(ns => !string.IsNullOrEmpty(ns))
            .Distinct()
            .ToList();

        // Dictionaries to store namespace foldouts
        Variables = scriptType.GetMinimalFieldList(Namespaces);
        Properties = scriptType.GetMinimalPropertyList(Namespaces);
        Functions = scriptType.GetMinimalMethodList(Namespaces);


        foreach (FieldInfo field in Variables)
        {
            NodeDataBase node = new NodeDataBase
            {
                Name = field.Name,
                DisplayName = ObjectNames.NicifyVariableName(field.Name),
                Info = field.FieldType.Name,
                NodeType = eNodeType.Variable,
                Type = field.FieldType,
                InputPorts = null,
                OutputPort = new NodePortBase()
                {
                    Name = "Value",
                    Type = field.FieldType
                }
            };
            Nodes.Add(node);
        }

        foreach (PropertyInfo property in Properties)
        {
            NodeDataBase node = new NodeDataBase
            {
                Name = property.Name,
                DisplayName = ObjectNames.NicifyVariableName(property.Name),
                Info = property.PropertyType.Name,
                NodeType = eNodeType.Property,
                Type = property.PropertyType,
                InputPorts = null,
                OutputPort = new NodePortBase()
                {
                    Name = "Value",
                    Type = property.PropertyType
                }
            };
            Nodes.Add(node);
        }

        foreach (MethodInfo method in Functions)
        {
            bool isStartMethod = method.Name == "Start" && method.ReturnType == typeof(void) && method.DeclaringType.IsSubclassOf(typeof(MonoBehaviour));
            NodeDataBase node = new NodeDataBase (!isStartMethod)
            {
                Name = method.Name,
                DisplayName = method.Name,
                Info = method.ReturnType.Name,
                NodeType = eNodeType.Function,
                Type = method.ReturnType,
                InputPorts = method.GetParameters().Select(p => new NodePortBase()
                {
                    Name = p.Name,
                    Type = p.ParameterType
                }).ToList(),
                OutputPort = new NodePortBase()
                {
                    Name = "Value",
                    Type = method.ReturnType
                }
            };
            Nodes.Add(node);
        }
    }
}
