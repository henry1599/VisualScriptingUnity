
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCast (Ray ray, Single radius, RaycastHit hitInfo, Single maxDistance, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/SphereCast",
            Deletable = true,
            Help = "SphereCast overload 9 of Physics"
        )]
        public class SphereCastNode9 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }