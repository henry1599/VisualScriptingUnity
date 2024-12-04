
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "GetAccumulatedTorque Overload 2",
    Path = "UnityEngine/Rigidbody/GetAccumulatedTorque",
    Deletable = true,
    Help = "GetAccumulatedTorque overload 2 of Rigidbody"
)]
public class GetAccumulatedTorqueNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output(Name = "result")] public Vector3 result;

    public override object OnRequestValue(Port port)
    {
        Vector3 result = rigidbody.GetAccumulatedTorque();
        return result;
    }
}