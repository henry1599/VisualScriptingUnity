
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/angularVelocity",
        Deletable = true,
        Help = "Setter for angularVelocity of Rigidbody2D"
    )]
    public class angularVelocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "angularVelocity", Editable = true)] public Single angularvelocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.angularVelocity = angularvelocity;
            return exit;
        }
    }
}