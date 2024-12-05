
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/totalForce",
        Deletable = true,
        Help = "Setter for totalForce of Rigidbody2D"
    )]
    public class totalForceSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "totalForce", Editable = true)] public Vector2 totalforce;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.totalForce = totalforce;
            return exit;
        }
    }
}