
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddForceAtPosition (Vector3 force, Vector3 position)",
            Path = "UnityEngine/Rigidbody/Methods/AddForceAtPosition",
            Deletable = true,
            Help = "AddForceAtPosition overload 2 of Rigidbody"
        )]
        public class AddForceAtPositionNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "force", Editable = true)] public Vector3 force;
    [Input(Name = "position", Editable = true)] public Vector3 position;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddForceAtPosition(force, position);
                return exit;
            }
        }
    }