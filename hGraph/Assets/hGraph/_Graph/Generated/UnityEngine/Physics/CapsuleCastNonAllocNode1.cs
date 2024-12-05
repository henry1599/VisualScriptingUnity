
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CapsuleCastNonAlloc (Vector3 point1, Vector3 point2, Single radius, Vector3 direction, RaycastHit[] results, Single maxDistance, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/CapsuleCastNonAlloc",
            Deletable = true,
            Help = "CapsuleCastNonAlloc overload 1 of Physics"
        )]
        public class CapsuleCastNonAllocNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "point1", Editable = true)] public Vector3 point1;
    [Input(Name = "point2", Editable = true)] public Vector3 point2;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }