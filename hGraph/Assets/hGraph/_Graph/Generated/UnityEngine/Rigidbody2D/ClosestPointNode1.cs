
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "ClosestPoint (Vector2 position)",
            Path = "UnityEngine/Rigidbody2D/Methods/ClosestPoint",
            Deletable = true,
            Help = "ClosestPoint overload 1 of Rigidbody2D"
        )]
        public class ClosestPointNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "position", Editable = true)] public Vector2 position;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.ClosestPoint(position);
                return result;
            }
        }
    }