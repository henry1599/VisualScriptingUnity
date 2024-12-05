
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetRelativeVector (Vector2 relativeVector)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetRelativeVector",
            Deletable = true,
            Help = "GetRelativeVector overload 1 of Rigidbody2D"
        )]
        public class GetRelativeVectorNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "relativeVector", Editable = true)] public Vector2 relativeVector;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.GetRelativeVector(relativeVector);
                return result;
            }
        }
    }