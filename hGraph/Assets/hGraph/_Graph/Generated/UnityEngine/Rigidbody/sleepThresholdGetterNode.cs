
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/sleepThreshold",
        Deletable = true,
        Help = "Getter for sleepThreshold of Rigidbody"
    )]
    public class sleepThresholdGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "sleepThreshold")] public Single sleepthreshold;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.sleepThreshold;
        }
    }
}