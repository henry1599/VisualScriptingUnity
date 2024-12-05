
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "BroadcastMessage (String methodName, object parameter, SendMessageOptions options)",
            Path = "UnityEngine/Rigidbody/Methods/BroadcastMessage",
            Deletable = true,
            Help = "BroadcastMessage overload 1 of Rigidbody"
        )]
        public class BroadcastMessageNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "parameter", Editable = true)] public object parameter;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.BroadcastMessage(methodName, parameter, options);
                return exit;
            }
        }
    }