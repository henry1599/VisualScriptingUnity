
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "ResetCenterOfMass Overload 1",
    Path = "UnityEngine/Rigidbody/ResetCenterOfMass",
    Deletable = true,
    Help = "ResetCenterOfMass overload 1 of Rigidbody"
)]
public class ResetCenterOfMassNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.ResetCenterOfMass();
        return exit;
    }
}