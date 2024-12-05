
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastAll (Ray ray, Single radius, Single maxDistance, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/SphereCastAll",
            Deletable = true,
            Help = "SphereCastAll overload 5 of Physics"
        )]
        public class SphereCastAllNode5 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }