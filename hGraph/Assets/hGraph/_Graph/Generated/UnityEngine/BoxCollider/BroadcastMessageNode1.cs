
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "BroadcastMessage (String methodName, Object parameter, SendMessageOptions options)",
            Path = "UnityEngine/BoxCollider/Methods/BroadcastMessage",
            Deletable = true,
            Help = "BroadcastMessage overload 1 of BoxCollider"
        )]
        public class BroadcastMessageNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "parameter", Editable = true)] public Object parameter;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                boxcollider.BroadcastMessage(methodName, parameter, options);
                return exit;
            }
        }
    }