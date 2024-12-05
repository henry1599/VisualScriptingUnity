
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "MoveRotation (Quaternion rot)",
            Path = "UnityEngine/Rigidbody/Methods/MoveRotation",
            Deletable = true,
            Help = "MoveRotation overload 1 of Rigidbody"
        )]
        public class MoveRotationNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "rot", Editable = true)] public Quaternion rot;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.MoveRotation(rot);
                return exit;
            }
        }
    }