
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/interpolation",
        Deletable = true,
        Help = "Setter for interpolation of Rigidbody2D"
    )]
    public class interpolationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "interpolation", Editable = true)] public RigidbodyInterpolation2D interpolation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.interpolation = interpolation;
            return exit;
        }
    }
}