
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddForce Overload 3",
    Path = "UnityEngine/Rigidbody/AddForce",
    Deletable = true,
    Help = "AddForce overload 3 of Rigidbody"
)]
public class AddForceNode3 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "x", Editable = true)] public Single x;
    [Input(Name = "y", Editable = true)] public Single y;
    [Input(Name = "z", Editable = true)] public Single z;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddForce(x, y, z, mode);
        return exit;
    }
}