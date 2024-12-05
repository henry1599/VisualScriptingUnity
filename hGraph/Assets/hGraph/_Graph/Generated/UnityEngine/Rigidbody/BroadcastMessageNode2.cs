
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "BroadcastMessage (String methodName, Object parameter)",
            Path = "UnityEngine/Rigidbody/Methods/BroadcastMessage",
            Deletable = true,
            Help = "BroadcastMessage overload 2 of Rigidbody"
        )]
        public class BroadcastMessageNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "parameter", Editable = true)] public Object parameter;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.BroadcastMessage(methodName, parameter);
                return exit;
            }
        }
    }