
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddTorque (Vector3 torque, ForceMode mode)",
            Path = "UnityEngine/Rigidbody/Methods/AddTorque",
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
    }