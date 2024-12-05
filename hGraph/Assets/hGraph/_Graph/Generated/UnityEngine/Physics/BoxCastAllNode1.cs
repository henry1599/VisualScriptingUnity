
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) BoxCastAll (Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, Single maxDistance, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/BoxCastAll",
            Deletable = true,
            Help = "BoxCastAll overload 1 of Physics"
        )]
        public class BoxCastAllNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "orientation", Editable = true)] public Quaternion orientation;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }