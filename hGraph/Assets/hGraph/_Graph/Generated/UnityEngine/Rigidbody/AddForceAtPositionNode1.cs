
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddForceAtPosition (Vector3 force, Vector3 position, ForceMode mode)",
            Path = "UnityEngine/Rigidbody/Methods/AddForceAtPosition",
            Deletable = true,
            Help = "AddForceAtPosition overload 1 of Rigidbody"
        )]
        public class AddForceAtPositionNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "force", Editable = true)] public Vector3 force;
    [Input(Name = "position", Editable = true)] public Vector3 position;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddForceAtPosition(force, position, mode);
                return exit;
            }
        }
    }