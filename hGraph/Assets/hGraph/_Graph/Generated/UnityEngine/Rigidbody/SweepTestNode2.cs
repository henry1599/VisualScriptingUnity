
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SweepTest Overload 2",
    Path = "UnityEngine/Rigidbody/SweepTest",
    Deletable = true,
    Help = "SweepTest overload 2 of Rigidbody"
)]
public class SweepTestNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

    [Output(Name = "result")] public Boolean result;

    public override object OnRequestValue(Port port)
    {
        Boolean result = rigidbody.SweepTest(direction, out hitInfo, maxDistance);
        return result;
    }
}