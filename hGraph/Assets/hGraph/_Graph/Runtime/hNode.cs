using System.Collections;
using System.Collections.Generic;
using BlueGraph;
using UnityEngine;

[Node]
public class hNode : Node
{
    public object OutputValue;
    public override object OnRequestValue(Port port)
    {
        return OutputValue;
    }
}
