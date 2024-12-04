
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddTorque Overload 1",
    Path = "UnityEngine/Rigidbody/AddTorque",
    Deletable = true,
    Help = "AddTorque overload 1 of Rigidbody"
)]
public class AddTorqueNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "torque", Editable = true)] public Vector3 torque;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddTorque(torque, mode);
        return exit;
    }
}