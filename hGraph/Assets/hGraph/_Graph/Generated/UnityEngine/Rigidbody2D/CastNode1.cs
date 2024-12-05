
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "Cast (Vector2 direction, RaycastHit2D[] results)",
            Path = "UnityEngine/Rigidbody2D/Methods/Cast",
            Deletable = true,
            Help = "Cast overload 1 of Rigidbody2D"
        )]
        public class CastNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "direction", Editable = true)] public Vector2 direction;
    [Input(Name = "results", Editable = true)] public RaycastHit2D[] results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.Cast(direction, results);
                return result;
            }
        }
    }