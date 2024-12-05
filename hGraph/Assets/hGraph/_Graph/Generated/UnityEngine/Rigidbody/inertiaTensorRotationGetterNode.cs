
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/inertiaTensorRotation",
        Deletable = true,
        Help = "Getter for inertiaTensorRotation of Rigidbody"
    )]
    public class inertiaTensorRotationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "inertiaTensorRotation")] public Quaternion inertiatensorrotation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.inertiaTensorRotation;
        }
    }
}