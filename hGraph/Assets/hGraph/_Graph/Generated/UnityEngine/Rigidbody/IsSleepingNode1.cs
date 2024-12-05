
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "IsSleeping",
            Path = "UnityEngine/Rigidbody/Methods",
            Deletable = true,
            Help = "IsSleeping overload 1 of Rigidbody"
        )]
        public class IsSleepingNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody.IsSleeping();
                return result;
            }
        }
    }