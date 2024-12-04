
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SetMaxAngularVelocity Overload 1",
    Path = "UnityEngine/Rigidbody/SetMaxAngularVelocity",
    Deletable = true,
    Help = "SetMaxAngularVelocity overload 1 of Rigidbody"
)]
public class SetMaxAngularVelocityNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "a", Editable = true)] public Single a;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.SetMaxAngularVelocity(a);
        return exit;
    }
}