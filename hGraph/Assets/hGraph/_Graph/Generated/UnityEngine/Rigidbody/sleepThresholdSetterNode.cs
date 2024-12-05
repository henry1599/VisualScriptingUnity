
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/sleepThreshold",
        Deletable = true,
        Help = "Setter for sleepThreshold of Rigidbody"
    )]
    public class sleepThresholdSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "sleepThreshold", Editable = true)] public Single sleepthreshold;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.sleepThreshold = sleepthreshold;
            return exit;
        }
    }
}