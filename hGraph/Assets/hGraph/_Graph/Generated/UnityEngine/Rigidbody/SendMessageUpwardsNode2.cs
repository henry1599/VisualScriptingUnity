
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SendMessageUpwards (String methodName, object value)",
            Path = "UnityEngine/Rigidbody/Methods/SendMessageUpwards",
            Deletable = true,
            Help = "SendMessageUpwards overload 2 of Rigidbody"
        )]
        public class SendMessageUpwardsNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public object value;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.SendMessageUpwards(methodName, value);
                return exit;
            }
        }
    }