
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/freezeRotation",
        Deletable = true,
        Help = "Getter for freezeRotation of Rigidbody2D"
    )]
    public class freezeRotationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "freezeRotation")] public Boolean freezerotation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.freezeRotation;
        }
    }
}