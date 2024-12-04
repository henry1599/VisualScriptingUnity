
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "GetAccumulatedForce Overload 2",
    Path = "UnityEngine/Rigidbody/GetAccumulatedForce",
    Deletable = true,
    Help = "GetAccumulatedForce overload 2 of Rigidbody"
)]
public class GetAccumulatedForceNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output(Name = "result")] public Vector3 result;

    public override object OnRequestValue(Port port)
    {
        Vector3 result = rigidbody.GetAccumulatedForce();
        return result;
    }
}