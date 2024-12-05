
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/interpolation",
        Deletable = true,
        Help = "Getter for interpolation of Rigidbody2D"
    )]
    public class interpolationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "interpolation")] public RigidbodyInterpolation2D interpolation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.interpolation;
        }
    }
}