
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/isKinematic",
        Deletable = true,
        Help = "Getter for isKinematic of Rigidbody2D"
    )]
    public class isKinematicGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "isKinematic")] public Boolean iskinematic;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.isKinematic;
        }
    }
}