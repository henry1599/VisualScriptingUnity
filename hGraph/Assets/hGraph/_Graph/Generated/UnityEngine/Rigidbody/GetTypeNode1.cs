
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "GetType Overload 1",
    Path = "UnityEngine/Rigidbody/GetType",
    Deletable = true,
    Help = "GetType overload 1 of Rigidbody"
)]
public class GetTypeNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output(Name = "result")] public Type result;

    public override object OnRequestValue(Port port)
    {
        Type result = rigidbody.GetType();
        return result;
    }
}