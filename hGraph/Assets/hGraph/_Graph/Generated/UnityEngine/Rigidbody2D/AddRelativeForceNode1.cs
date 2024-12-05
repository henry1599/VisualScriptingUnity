
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "AddRelativeForce (Vector2 relativeForce)",
            Path = "UnityEngine/Rigidbody2D/Methods/AddRelativeForce",
            Deletable = true,
            Help = "AddRelativeForce overload 1 of Rigidbody2D"
        )]
        public class AddRelativeForceNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "relativeForce", Editable = true)] public Vector2 relativeForce;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.AddRelativeForce(relativeForce);
                return exit;
            }
        }
    }