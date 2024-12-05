
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SendMessageUpwards (String methodName)",
            Path = "UnityEngine/Rigidbody/Methods/SendMessageUpwards",
            Deletable = true,
            Help = "SendMessageUpwards overload 3 of Rigidbody"
        )]
        public class SendMessageUpwardsNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "methodName", Editable = true)] public String methodName;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.SendMessageUpwards(methodName);
                return exit;
            }
        }
    }