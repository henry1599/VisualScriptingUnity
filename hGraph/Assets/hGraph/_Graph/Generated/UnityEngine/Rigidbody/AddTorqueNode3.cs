
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddTorque (Single x, Single y, Single z, ForceMode mode)",
            Path = "UnityEngine/Rigidbody/Methods/AddTorque",
            Deletable = true,
            Help = "AddTorque overload 3 of Rigidbody"
        )]
        public class AddTorqueNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "x", Editable = true)] public Single x;
    [Input(Name = "y", Editable = true)] public Single y;
    [Input(Name = "z", Editable = true)] public Single z;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddTorque(x, y, z, mode);
                return exit;
            }
        }
    }