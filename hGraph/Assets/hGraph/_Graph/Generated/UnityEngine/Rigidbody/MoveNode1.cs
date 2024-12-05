
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "Move (Vector3 position, Quaternion rotation)",
            Path = "UnityEngine/Rigidbody/Methods/Move",
            Deletable = true,
            Help = "Move overload 1 of Rigidbody"
        )]
        public class MoveNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "position", Editable = true)] public Vector3 position;
    [Input(Name = "rotation", Editable = true)] public Quaternion rotation;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.Move(position, rotation);
                return exit;
            }
        }
    }