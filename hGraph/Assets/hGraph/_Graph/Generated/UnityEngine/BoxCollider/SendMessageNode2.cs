
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "SendMessage (String methodName)",
            Path = "UnityEngine/BoxCollider/Methods/SendMessage",
            Deletable = true,
            Help = "SendMessage overload 2 of BoxCollider"
        )]
        public class SendMessageNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "methodName", Editable = true)] public String methodName;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                boxcollider.SendMessage(methodName);
                return exit;
            }
        }
    }