
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "BroadcastMessage (String methodName, SendMessageOptions options)",
            Path = "UnityEngine/Rigidbody2D/Methods/BroadcastMessage",
            Deletable = true,
            Help = "BroadcastMessage overload 4 of Rigidbody2D"
        )]
        public class BroadcastMessageNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.BroadcastMessage(methodName, options);
                return exit;
            }
        }
    }