
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapCapsule (Vector3 point0, Vector3 point1, Single radius, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/OverlapCapsule",
            Deletable = true,
            Help = "OverlapCapsule overload 1 of Physics"
        )]
        public class OverlapCapsuleNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "point0", Editable = true)] public Vector3 point0;
    [Input(Name = "point1", Editable = true)] public Vector3 point1;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Collider[] result;

            public override object OnRequestValue(Port port)
            {
                Collider[] result = Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }