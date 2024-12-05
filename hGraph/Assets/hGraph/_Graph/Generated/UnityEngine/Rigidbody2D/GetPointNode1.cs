
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetPoint (Vector2 point)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetPoint",
            Deletable = true,
            Help = "GetPoint overload 1 of Rigidbody2D"
        )]
        public class GetPointNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "point", Editable = true)] public Vector2 point;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.GetPoint(point);
                return result;
            }
        }
    }