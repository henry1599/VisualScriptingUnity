
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CheckCapsule (Vector3 start, Vector3 end, Single radius, Int32 layerMask, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Physics/Methods/CheckCapsule",
            Deletable = true,
            Help = "CheckCapsule overload 1 of Physics"
        )]
        public class CheckCapsuleNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "start", Editable = true)] public Vector3 start;
    [Input(Name = "end", Editable = true)] public Vector3 end;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.CheckCapsule(start, end, radius, layerMask, queryTriggerInteraction);
                return result;
            }
        }
    }