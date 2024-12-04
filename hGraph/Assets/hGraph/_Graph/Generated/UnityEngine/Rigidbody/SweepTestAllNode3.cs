
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SweepTestAll Overload 3",
    Path = "UnityEngine/Rigidbody/SweepTestAll",
    Deletable = true,
    Help = "SweepTestAll overload 3 of Rigidbody"
)]
public class SweepTestAllNode3 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;

    [Output(Name = "result")] public RaycastHit[] result;

    public override object OnRequestValue(Port port)
    {
        RaycastHit[] result = rigidbody.SweepTestAll(direction);
        return result;
    }
}