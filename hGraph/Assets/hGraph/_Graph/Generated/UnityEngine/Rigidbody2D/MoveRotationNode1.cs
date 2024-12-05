
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "MoveRotation (Single angle)",
            Path = "UnityEngine/Rigidbody2D/Methods/MoveRotation",
            Deletable = true,
            Help = "MoveRotation overload 1 of Rigidbody2D"
        )]
        public class MoveRotationNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "angle", Editable = true)] public Single angle;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.MoveRotation(angle);
                return exit;
            }
        }
    }