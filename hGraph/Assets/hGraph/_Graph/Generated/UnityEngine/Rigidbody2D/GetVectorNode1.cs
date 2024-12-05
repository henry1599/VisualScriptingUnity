
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetVector (Vector2 vector)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetVector",
            Deletable = true,
            Help = "GetVector overload 1 of Rigidbody2D"
        )]
        public class GetVectorNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "vector", Editable = true)] public Vector2 vector;

            [Output(Name = "result")] public Vector2 result;

            public override object OnRequestValue(Port port)
            {
                Vector2 result = rigidbody2d.GetVector(vector);
                return result;
            }
        }
    }