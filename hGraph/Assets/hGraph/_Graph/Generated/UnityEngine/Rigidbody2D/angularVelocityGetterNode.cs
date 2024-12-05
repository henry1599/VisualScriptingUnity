
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/angularVelocity",
        Deletable = true,
        Help = "Getter for angularVelocity of Rigidbody2D"
    )]
    public class angularVelocityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "angularVelocity")] public Single angularvelocity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.angularVelocity;
        }
    }
}