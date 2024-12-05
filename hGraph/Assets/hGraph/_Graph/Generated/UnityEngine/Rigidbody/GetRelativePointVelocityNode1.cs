
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "GetRelativePointVelocity (Vector3 relativePoint)",
            Path = "UnityEngine/Rigidbody/Methods/GetRelativePointVelocity",
            Deletable = true,
            Help = "GetRelativePointVelocity overload 1 of Rigidbody"
        )]
        public class GetRelativePointVelocityNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "relativePoint", Editable = true)] public Vector3 relativePoint;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = rigidbody.GetRelativePointVelocity(relativePoint);
                return result;
            }
        }
    }