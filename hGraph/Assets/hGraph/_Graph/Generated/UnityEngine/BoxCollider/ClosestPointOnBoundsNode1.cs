
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "ClosestPointOnBounds (Vector3 position)",
            Path = "UnityEngine/BoxCollider/Methods/ClosestPointOnBounds",
            Deletable = true,
            Help = "ClosestPointOnBounds overload 1 of BoxCollider"
        )]
        public class ClosestPointOnBoundsNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "position", Editable = true)] public Vector3 position;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = boxcollider.ClosestPointOnBounds(position);
                return result;
            }
        }
    }