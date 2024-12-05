
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RaycastNonAlloc (Vector3 origin, Vector3 direction, RaycastHit[] results, Single maxDistance, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/RaycastNonAlloc",
            Deletable = true,
            Help = "RaycastNonAlloc overload 5 of Physics"
        )]
        public class RaycastNonAllocNode5 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.RaycastNonAlloc(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }