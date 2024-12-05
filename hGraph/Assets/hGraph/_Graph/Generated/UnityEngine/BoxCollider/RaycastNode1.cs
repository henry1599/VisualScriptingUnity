
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "Raycast (Ray ray, RaycastHit hitInfo, Single maxDistance)",
            Path = "UnityEngine/BoxCollider/Methods/Raycast",
            Deletable = true,
            Help = "Raycast overload 1 of BoxCollider"
        )]
        public class RaycastNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = boxcollider.Raycast(ray, out hitInfo, maxDistance);
                return result;
            }
        }
    }