
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetRelativePointVelocity (Vector2 relativePoint)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetRelativePointVelocity",
            Deletable = true,
            Help = "GetRelativePointVelocity overload 1 of Rigidbody2D"
        )]
        public class GetRelativePointVelocityNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "relativePoint", Editable = true)] public Vector2 relativePoint;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.GetRelativePointVelocity(relativePoint);
                return result;
            }
        }
    }