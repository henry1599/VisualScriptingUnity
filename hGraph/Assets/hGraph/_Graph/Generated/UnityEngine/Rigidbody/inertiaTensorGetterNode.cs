
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/inertiaTensor",
        Deletable = true,
        Help = "Getter for inertiaTensor of Rigidbody"
    )]
    public class inertiaTensorGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "inertiaTensor")] public Vector3 inertiatensor;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.inertiaTensor;
        }
    }
}