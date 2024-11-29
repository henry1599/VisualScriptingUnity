using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlueGraph;

[Node(Path = "Math/Add", Name = "Add")]
public class hNode_Add : Node
{
    [Input] public float a;
    [Input] public float b;
    [Output] public float result;
    public override object OnRequestValue(Port port)
    {
        float aValue = GetInputValue<float>(nameof(this.a), this.a);
        float bValue = GetInputValue<float>(nameof(this.b), this.b);
        return aValue + bValue;
    }
}

[Node(Path = "Monobehaviour", Name = "Start")]
public class hNode_Start : Node
{
    public override void OnEnable()
    {
        base.OnEnable();
        HasEntry = false;
    }
    public override object OnRequestValue(Port port)
    {
        return this;
    }
}

