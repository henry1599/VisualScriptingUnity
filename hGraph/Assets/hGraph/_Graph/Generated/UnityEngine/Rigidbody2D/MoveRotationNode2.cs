
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "MoveRotation (Quaternion rotation)",
            Path = "UnityEngine/Rigidbody2D/Methods/MoveRotation",
            Deletable = true,
            Help = "MoveRotation overload 2 of Rigidbody2D"
        )]
        public class MoveRotationNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "rotation", Editable = true)] public Quaternion rotation;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.MoveRotation(rotation);
                return exit;
            }
        }
    }