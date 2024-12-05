
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "GetPointVelocity (Vector3 worldPoint)",
            Path = "UnityEngine/Rigidbody/Methods/GetPointVelocity",
            Deletable = true,
            Help = "GetPointVelocity overload 1 of Rigidbody"
        )]
        public class GetPointVelocityNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "worldPoint", Editable = true)] public Vector3 worldPoint;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = rigidbody.GetPointVelocity(worldPoint);
                return result;
            }
        }
    }