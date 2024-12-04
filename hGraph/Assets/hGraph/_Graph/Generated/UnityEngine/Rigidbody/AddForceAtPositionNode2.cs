
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddForceAtPosition Overload 2",
    Path = "UnityEngine/Rigidbody/AddForceAtPosition",
    Deletable = true,
    Help = "AddForceAtPosition overload 2 of Rigidbody"
)]
public class AddForceAtPositionNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "force", Editable = true)] public Vector3 force;
    [Input(Name = "position", Editable = true)] public Vector3 position;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddForceAtPosition(force, position);
        return exit;
    }
}