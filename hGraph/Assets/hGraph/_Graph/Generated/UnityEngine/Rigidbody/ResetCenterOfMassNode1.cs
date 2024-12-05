
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "ResetCenterOfMass",
            Path = "UnityEngine/Rigidbody/Methods",
            Deletable = true,
            Help = "ResetCenterOfMass overload 1 of Rigidbody"
        )]
        public class ResetCenterOfMassNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.ResetCenterOfMass();
                return exit;
            }
        }
    }