
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/maxAngularVelocity",
        Deletable = true,
        Help = "Getter for maxAngularVelocity of Rigidbody"
    )]
    public class maxAngularVelocityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "maxAngularVelocity")] public Single maxangularvelocity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.maxAngularVelocity;
        }
    }
}