
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "ClosestPoint (Vector3 position)",
            Path = "UnityEngine/BoxCollider/Methods/ClosestPoint",
            Deletable = true,
            Help = "ClosestPoint overload 1 of BoxCollider"
        )]
        public class ClosestPointNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "position", Editable = true)] public Vector3 position;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = boxcollider.ClosestPoint(position);
                return result;
            }
        }
    }