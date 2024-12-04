
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "MovePosition Overload 1",
    Path = "UnityEngine/Rigidbody/MovePosition",
    Deletable = true,
    Help = "MovePosition overload 1 of Rigidbody"
)]
public class MovePositionNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "position", Editable = true)] public Vector3 position;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.MovePosition(position);
        return exit;
    }
}