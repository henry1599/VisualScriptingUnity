
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/maxLinearVelocity",
        Deletable = true,
        Help = "Getter for maxLinearVelocity of Rigidbody"
    )]
    public class maxLinearVelocityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "maxLinearVelocity")] public Single maxlinearvelocity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.maxLinearVelocity;
        }
    }
}