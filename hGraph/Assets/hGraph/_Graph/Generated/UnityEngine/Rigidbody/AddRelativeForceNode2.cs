
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddRelativeForce Overload 2",
    Path = "UnityEngine/Rigidbody/AddRelativeForce",
    Deletable = true,
    Help = "AddRelativeForce overload 2 of Rigidbody"
)]
public class AddRelativeForceNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "force", Editable = true)] public Vector3 force;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddRelativeForce(force);
        return exit;
    }
}