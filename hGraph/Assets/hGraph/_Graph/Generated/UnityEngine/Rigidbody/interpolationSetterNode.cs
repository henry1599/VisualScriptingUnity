
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/interpolation",
        Deletable = true,
        Help = "Setter for interpolation of Rigidbody"
    )]
    public class interpolationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "interpolation", Editable = true)] public RigidbodyInterpolation interpolation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.interpolation = interpolation;
            return exit;
        }
    }
}