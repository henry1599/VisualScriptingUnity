
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "SendMessageUpwards (String methodName)",
            Path = "UnityEngine/Rigidbody2D/Methods/SendMessageUpwards",
            Deletable = true,
            Help = "SendMessageUpwards overload 3 of Rigidbody2D"
        )]
        public class SendMessageUpwardsNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "methodName", Editable = true)] public String methodName;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.SendMessageUpwards(methodName);
                return exit;
            }
        }
    }