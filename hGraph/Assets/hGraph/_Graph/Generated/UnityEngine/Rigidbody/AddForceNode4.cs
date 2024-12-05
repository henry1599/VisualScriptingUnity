
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddForce (Single x, Single y, Single z)",
            Path = "UnityEngine/Rigidbody/Methods/AddForce",
            Deletable = true,
            Help = "AddForce overload 4 of Rigidbody"
        )]
        public class AddForceNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "x", Editable = true)] public Single x;
    [Input(Name = "y", Editable = true)] public Single y;
    [Input(Name = "z", Editable = true)] public Single z;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddForce(x, y, z);
                return exit;
            }
        }
    }