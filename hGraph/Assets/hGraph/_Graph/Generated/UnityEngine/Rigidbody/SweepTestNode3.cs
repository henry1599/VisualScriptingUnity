
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SweepTest Overload 3",
    Path = "UnityEngine/Rigidbody/SweepTest",
    Deletable = true,
    Help = "SweepTest overload 3 of Rigidbody"
)]
public class SweepTestNode3 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;

    [Output(Name = "result")] public Boolean result;

    public override object OnRequestValue(Port port)
    {
        Boolean result = rigidbody.SweepTest(direction, out hitInfo);
        return result;
    }
}