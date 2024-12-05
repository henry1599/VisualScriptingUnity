
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/velocity",
        Deletable = true,
        Help = "Setter for velocity of Rigidbody2D"
    )]
    public class velocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "velocity", Editable = true)] public Vector2 velocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.velocity = velocity;
            return exit;
        }
    }
}