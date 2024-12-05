
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "SetMaxAngularVelocity (Single a)",
            Path = "UnityEngine/Rigidbody/Methods/SetMaxAngularVelocity",
            Deletable = true,
            Help = "SetMaxAngularVelocity overload 1 of Rigidbody"
        )]
        public class SetMaxAngularVelocityNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "a", Editable = true)] public Single a;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.SetMaxAngularVelocity(a);
                return exit;
            }
        }
    }