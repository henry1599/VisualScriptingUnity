
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "AddForce (Vector2 force, ForceMode2D mode)",
            Path = "UnityEngine/Rigidbody2D/Methods/AddForce",
            Deletable = true,
            Help = "AddForce overload 2 of Rigidbody2D"
        )]
        public class AddForceNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "force", Editable = true)] public Vector2 force;
    [Input(Name = "mode", Editable = true)] public ForceMode2D mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.AddForce(force, mode);
                return exit;
            }
        }
    }