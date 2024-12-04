
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SetDensity Overload 1",
    Path = "UnityEngine/Rigidbody/SetDensity",
    Deletable = true,
    Help = "SetDensity overload 1 of Rigidbody"
)]
public class SetDensityNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "density", Editable = true)] public Single density;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.SetDensity(density);
        return exit;
    }
}