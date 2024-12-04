
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddRelativeForce Overload 1",
    Path = "UnityEngine/Rigidbody/AddRelativeForce",
    Deletable = true,
    Help = "AddRelativeForce overload 1 of Rigidbody"
)]
public class AddRelativeForceNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "force", Editable = true)] public Vector3 force;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddRelativeForce(force, mode);
        return exit;
    }
}