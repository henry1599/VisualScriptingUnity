
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "SendMessage (String methodName, Object value)",
            Path = "UnityEngine/Rigidbody2D/Methods/SendMessage",
            Deletable = true,
            Help = "SendMessage overload 1 of Rigidbody2D"
        )]
        public class SendMessageNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public Object value;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.SendMessage(methodName, value);
                return exit;
            }
        }
    }