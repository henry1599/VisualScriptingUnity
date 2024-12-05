
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddRelativeTorque (Single x, Single y, Single z)",
            Path = "UnityEngine/Rigidbody/Methods/AddRelativeTorque",
            Deletable = true,
            Help = "AddRelativeTorque overload 4 of Rigidbody"
        )]
        public class AddRelativeTorqueNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "x", Editable = true)] public Single x;
    [Input(Name = "y", Editable = true)] public Single y;
    [Input(Name = "z", Editable = true)] public Single z;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddRelativeTorque(x, y, z);
                return exit;
            }
        }
    }