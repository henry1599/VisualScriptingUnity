
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapBoxNonAlloc (Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, Int32 mask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/OverlapBoxNonAlloc",
            Deletable = true,
            Help = "OverlapBoxNonAlloc overload 1 of Physics"
        )]
        public class OverlapBoxNonAllocNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "results", Editable = true)] public Collider[] results;
    [Input(Name = "orientation", Editable = true)] public Quaternion orientation;
    [Input(Name = "mask", Editable = true)] public Int32 mask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
                return result;
            }
        }
    }