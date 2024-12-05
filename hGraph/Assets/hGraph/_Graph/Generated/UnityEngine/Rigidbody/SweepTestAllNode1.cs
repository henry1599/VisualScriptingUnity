
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SweepTestAll (Vector3 direction, Single maxDistance, QueryTriggerInteraction queryTriggerInteraction)",
            Path = "UnityEngine/Rigidbody/Methods/SweepTestAll",
            Deletable = true,
            Help = "SweepTestAll overload 1 of Rigidbody"
        )]
        public class SweepTestAllNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "queryTriggerInteraction", Editable = true)] public QueryTriggerInteraction queryTriggerInteraction;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = rigidbody.SweepTestAll(direction, maxDistance, queryTriggerInteraction);
                return result;
            }
        }
    }