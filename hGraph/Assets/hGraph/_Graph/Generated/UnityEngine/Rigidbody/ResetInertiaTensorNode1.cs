
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "ResetInertiaTensor",
            Path = "UnityEngine/Rigidbody/Methods",
            Deletable = true,
            Help = "ResetInertiaTensor overload 1 of Rigidbody"
        )]
        public class ResetInertiaTensorNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.ResetInertiaTensor();
                return exit;
            }
        }
    }