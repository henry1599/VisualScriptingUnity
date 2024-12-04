
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddRelativeTorque Overload 2",
    Path = "UnityEngine/Rigidbody/AddRelativeTorque",
    Deletable = true,
    Help = "AddRelativeTorque overload 2 of Rigidbody"
)]
public class AddRelativeTorqueNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "torque", Editable = true)] public Vector3 torque;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddRelativeTorque(torque);
        return exit;
    }
}