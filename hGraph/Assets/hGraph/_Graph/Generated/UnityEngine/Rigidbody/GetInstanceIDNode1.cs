
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "GetInstanceID Overload 1",
    Path = "UnityEngine/Rigidbody/GetInstanceID",
    Deletable = true,
    Help = "GetInstanceID overload 1 of Rigidbody"
)]
public class GetInstanceIDNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output(Name = "result")] public Int32 result;

    public override object OnRequestValue(Port port)
    {
        Int32 result = rigidbody.GetInstanceID();
        return result;
    }
}