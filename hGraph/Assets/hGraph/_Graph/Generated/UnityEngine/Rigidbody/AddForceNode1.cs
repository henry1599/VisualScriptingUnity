
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddForce (Vector3 force, ForceMode mode)",
            Path = "UnityEngine/Rigidbody/Methods/AddForce",
            Deletable = true,
            Help = "AddForce overload 1 of Rigidbody"
        )]
        public class AddForceNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "force", Editable = true)] public Vector3 force;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddForce(force, mode);
                return exit;
            }
        }
    }