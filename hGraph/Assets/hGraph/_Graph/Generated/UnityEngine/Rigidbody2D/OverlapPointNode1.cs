
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "OverlapPoint (Vector2 point)",
            Path = "UnityEngine/Rigidbody2D/Methods/OverlapPoint",
            Deletable = true,
            Help = "OverlapPoint overload 1 of Rigidbody2D"
        )]
        public class OverlapPointNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "point", Editable = true)] public Vector2 point;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.OverlapPoint(point);
                return result;
            }
        }
    }