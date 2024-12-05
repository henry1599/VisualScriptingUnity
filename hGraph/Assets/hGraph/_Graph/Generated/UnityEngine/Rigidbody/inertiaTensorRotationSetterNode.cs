
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/inertiaTensorRotation",
        Deletable = true,
        Help = "Setter for inertiaTensorRotation of Rigidbody"
    )]
    public class inertiaTensorRotationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "inertiaTensorRotation", Editable = true)] public Quaternion inertiatensorrotation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.inertiaTensorRotation = inertiatensorrotation;
            return exit;
        }
    }
}