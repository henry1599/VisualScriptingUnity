
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SendMessageUpwards (String methodName, Object value, SendMessageOptions options)",
            Path = "UnityEngine/Rigidbody/Methods/SendMessageUpwards",
            Deletable = true,
            Help = "SendMessageUpwards overload 1 of Rigidbody"
        )]
        public class SendMessageUpwardsNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public Object value;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.SendMessageUpwards(methodName, value, options);
                return exit;
            }
        }
    }