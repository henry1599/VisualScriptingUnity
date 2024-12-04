
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SweepTestAll Overload 1",
    Path = "UnityEngine/Rigidbody/SweepTestAll",
    Deletable = true,
    Help = "SweepTestAll overload 1 of Rigidbody"
)]
public class SweepTestAllNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

    [Output(Name = "result")] public RaycastHit[] result;

    public override object OnRequestValue(Port port)
    {
        RaycastHit[] result = rigidbody.SweepTestAll(direction, maxDistance, queryTriggerInteraction);
        return result;
    }
}