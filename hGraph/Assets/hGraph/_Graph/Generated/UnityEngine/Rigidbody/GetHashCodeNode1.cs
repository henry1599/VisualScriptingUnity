
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "GetHashCode Overload 1",
    Path = "UnityEngine/Rigidbody/GetHashCode",
    Deletable = true,
    Help = "GetHashCode overload 1 of Rigidbody"
)]
public class GetHashCodeNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output(Name = "result")] public Int32 result;

    public override object OnRequestValue(Port port)
    {
        Int32 result = rigidbody.GetHashCode();
        return result;
    }
}