
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/maxDepenetrationVelocity",
        Deletable = true,
        Help = "Getter for maxDepenetrationVelocity of Rigidbody"
    )]
    public class maxDepenetrationVelocityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "maxDepenetrationVelocity")] public Single maxdepenetrationvelocity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.maxDepenetrationVelocity;
        }
    }
}