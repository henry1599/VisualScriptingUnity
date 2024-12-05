
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "AddForceAtPosition (Vector2 force, Vector2 position, ForceMode2D mode)",
            Path = "UnityEngine/Rigidbody2D/Methods/AddForceAtPosition",
            Deletable = true,
            Help = "AddForceAtPosition overload 2 of Rigidbody2D"
        )]
        public class AddForceAtPositionNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "force", Editable = true)] public Vector2 force;
    [Input(Name = "position", Editable = true)] public Vector2 position;
    [Input(Name = "mode", Editable = true)] public ForceMode2D mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.AddForceAtPosition(force, position, mode);
                return exit;
            }
        }
    }