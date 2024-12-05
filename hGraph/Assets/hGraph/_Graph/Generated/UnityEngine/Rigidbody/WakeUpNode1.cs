
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "WakeUp",
            Path = "UnityEngine/Rigidbody/Methods",
            Deletable = true,
            Help = "WakeUp overload 1 of Rigidbody"
        )]
        public class WakeUpNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.WakeUp();
                return exit;
            }
        }
    }