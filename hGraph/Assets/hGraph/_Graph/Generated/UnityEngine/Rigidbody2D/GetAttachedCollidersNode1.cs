
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetAttachedColliders (Collider2D[] results)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetAttachedColliders",
            Deletable = true,
            Help = "GetAttachedColliders overload 1 of Rigidbody2D"
        )]
        public class GetAttachedCollidersNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "results", Editable = true)] public Collider2D[] results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.GetAttachedColliders(results);
                return result;
            }
        }
    }