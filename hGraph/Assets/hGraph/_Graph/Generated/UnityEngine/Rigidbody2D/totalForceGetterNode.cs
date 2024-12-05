
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/totalForce",
        Deletable = true,
        Help = "Getter for totalForce of Rigidbody2D"
    )]
    public class totalForceGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "totalForce")] public Vector2 totalforce;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.totalForce;
        }
    }
}