
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetRelativePoint (Vector2 relativePoint)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetRelativePoint",
            Deletable = true,
            Help = "GetRelativePoint overload 1 of Rigidbody2D"
        )]
        public class GetRelativePointNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "relativePoint", Editable = true)] public Vector2 relativePoint;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.GetRelativePoint(relativePoint);
                return result;
            }
        }
    }