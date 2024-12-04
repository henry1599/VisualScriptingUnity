
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddRelativeForce Overload 4",
    Path = "UnityEngine/Rigidbody/AddRelativeForce",
    Deletable = true,
    Help = "AddRelativeForce overload 4 of Rigidbody"
)]
public class AddRelativeForceNode4 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "x", Editable = true)] public Single x;
    [Input(Name = "y", Editable = true)] public Single y;
    [Input(Name = "z", Editable = true)] public Single z;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddRelativeForce(x, y, z);
        return exit;
    }
}