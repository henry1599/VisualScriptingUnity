
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "ClosestPointOnBounds (Vector3 position)",
            Path = "UnityEngine/Rigidbody/Methods/ClosestPointOnBounds",
            Deletable = true,
            Help = "ClosestPointOnBounds overload 1 of Rigidbody"
        )]
        public class ClosestPointOnBoundsNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "position", Editable = true)] public Vector3 position;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = rigidbody.ClosestPointOnBounds(position);
                return result;
            }
        }
    }