
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/interpolation",
        Deletable = true,
        Help = "Getter for interpolation of Rigidbody"
    )]
    public class interpolationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "interpolation")] public RigidbodyInterpolation interpolation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.interpolation;
        }
    }
}