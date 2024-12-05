
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "BroadcastMessage (String methodName, Object parameter)",
            Path = "UnityEngine/BoxCollider/Methods/BroadcastMessage",
            Deletable = true,
            Help = "BroadcastMessage overload 2 of BoxCollider"
        )]
        public class BroadcastMessageNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "parameter", Editable = true)] public Object parameter;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                boxcollider.BroadcastMessage(methodName, parameter);
                return exit;
            }
        }
    }