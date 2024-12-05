
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "SendMessageUpwards (String methodName)",
            Path = "UnityEngine/BoxCollider/Methods/SendMessageUpwards",
            Deletable = true,
            Help = "SendMessageUpwards overload 3 of BoxCollider"
        )]
        public class SendMessageUpwardsNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "methodName", Editable = true)] public String methodName;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                boxcollider.SendMessageUpwards(methodName);
                return exit;
            }
        }
    }