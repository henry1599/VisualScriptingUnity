
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddTorque Overload 2",
    Path = "UnityEngine/Rigidbody/AddTorque",
    Deletable = true,
    Help = "AddTorque overload 2 of Rigidbody"
)]
public class AddTorqueNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "torque", Editable = true)] public Vector3 torque;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddTorque(torque);
        return exit;
    }
}