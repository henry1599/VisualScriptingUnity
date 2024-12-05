
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/rotation",
        Deletable = true,
        Help = "Getter for rotation of Rigidbody"
    )]
    public class rotationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "rotation")] public Quaternion rotation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.rotation;
        }
    }
}