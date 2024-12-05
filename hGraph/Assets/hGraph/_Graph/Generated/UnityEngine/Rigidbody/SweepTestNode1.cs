
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SweepTest (Vector3 direction, RaycastHit hitInfo, Single maxDistance, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Rigidbody/Methods/SweepTest",
            Deletable = true,
            Help = "SweepTest overload 1 of Rigidbody"
        )]
        public class SweepTestNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody.SweepTest(direction, out hitInfo, maxDistance, queryTriggerInteraction);
                return result;
            }
        }
    }