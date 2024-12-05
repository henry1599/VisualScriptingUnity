
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetPointVelocity (Vector2 point)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetPointVelocity",
            Deletable = true,
            Help = "GetPointVelocity overload 1 of Rigidbody2D"
        )]
        public class GetPointVelocityNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "point", Editable = true)] public Vector2 point;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.GetPointVelocity(point);
                return result;
            }
        }
    }