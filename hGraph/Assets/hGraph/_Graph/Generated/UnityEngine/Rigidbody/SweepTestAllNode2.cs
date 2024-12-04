
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SweepTestAll Overload 2",
    Path = "UnityEngine/Rigidbody/SweepTestAll",
    Deletable = true,
    Help = "SweepTestAll overload 2 of Rigidbody"
)]
public class SweepTestAllNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

    [Output(Name = "result")] public RaycastHit[] result;

    public override object OnRequestValue(Port port)
    {
        RaycastHit[] result = rigidbody.SweepTestAll(direction, maxDistance);
        return result;
    }
}