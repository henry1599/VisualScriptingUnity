
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "Equals Overload 1",
    Path = "UnityEngine/Rigidbody/Equals",
    Deletable = true,
    Help = "Equals overload 1 of Rigidbody"
)]
public class EqualsNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "other", Editable = true)] public Object other;

    [Output(Name = "result")] public Boolean result;

    public override object OnRequestValue(Port port)
    {
        Boolean result = rigidbody.Equals(other);
        return result;
    }
}