
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "AddForce (Vector2 force)",
            Path = "UnityEngine/Rigidbody2D/Methods/AddForce",
            Deletable = true,
            Help = "AddForce overload 1 of Rigidbody2D"
        )]
        public class AddForceNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "force", Editable = true)] public Vector2 force;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.AddForce(force);
                return exit;
            }
        }
    }