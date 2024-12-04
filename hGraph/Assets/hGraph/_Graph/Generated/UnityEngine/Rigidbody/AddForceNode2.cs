
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddForce Overload 2",
    Path = "UnityEngine/Rigidbody/AddForce",
    Deletable = true,
    Help = "AddForce overload 2 of Rigidbody"
)]
public class AddForceNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "force", Editable = true)] public Vector3 force;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddForce(force);
        return exit;
    }
}