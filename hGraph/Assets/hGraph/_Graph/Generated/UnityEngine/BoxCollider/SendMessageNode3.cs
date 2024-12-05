
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "SendMessage (String methodName, Object value, SendMessageOptions options)",
            Path = "UnityEngine/BoxCollider/Methods/SendMessage",
            Deletable = true,
            Help = "SendMessage overload 3 of BoxCollider"
        )]
        public class SendMessageNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public Object value;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                boxcollider.SendMessage(methodName, value, options);
                return exit;
            }
        }
    }