
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "GetAccumulatedTorque (Single step)",
            Path = "UnityEngine/Rigidbody/Methods/GetAccumulatedTorque",
            Deletable = true,
            Help = "GetAccumulatedTorque overload 1 of Rigidbody"
        )]
        public class GetAccumulatedTorqueNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "step", Editable = true)] public Single step;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = rigidbody.GetAccumulatedTorque(step);
                return result;
            }
        }
    }