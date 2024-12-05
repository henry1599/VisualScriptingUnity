
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "SendMessageUpwards (String methodName, Object value)",
            Path = "UnityEngine/BoxCollider/Methods/SendMessageUpwards",
            Deletable = true,
            Help = "SendMessageUpwards overload 2 of BoxCollider"
        )]
        public class SendMessageUpwardsNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public Object value;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                boxcollider.SendMessageUpwards(methodName, value);
                return exit;
            }
        }
    }