
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "Distance (Collider2D collider)",
            Path = "UnityEngine/Rigidbody2D/Methods/Distance",
            Deletable = true,
            Help = "Distance overload 1 of Rigidbody2D"
        )]
        public class DistanceNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "collider", Editable = true)] public Collider2D collider;

            [Output(Name = "result")] public ColliderDistance2D result;

            public override object OnRequestValue(Port port)
            {
                ColliderDistance2D result = rigidbody2d.Distance(collider);
                return result;
            }
        }
    }