
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SendMessage (String methodName)",
            Path = "UnityEngine/Rigidbody/Methods/SendMessage",
            Deletable = true,
            Help = "SendMessage overload 2 of Rigidbody"
        )]
        public class SendMessageNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "methodName", Editable = true)] public String methodName;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.SendMessage(methodName);
                return exit;
            }
        }
    }