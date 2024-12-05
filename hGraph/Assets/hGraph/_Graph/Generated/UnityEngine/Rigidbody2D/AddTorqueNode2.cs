
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "AddTorque (Single torque, ForceMode2D mode)",
            Path = "UnityEngine/Rigidbody2D/Methods/AddTorque",
            Deletable = true,
            Help = "AddTorque overload 2 of Rigidbody2D"
        )]
        public class AddTorqueNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "torque", Editable = true)] public Single torque;
    [Input(Name = "mode", Editable = true)] public ForceMode2D mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.AddTorque(torque, mode);
                return exit;
            }
        }
    }