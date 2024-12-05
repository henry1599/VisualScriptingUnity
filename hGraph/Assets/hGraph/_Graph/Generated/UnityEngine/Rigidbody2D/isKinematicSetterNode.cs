
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/isKinematic",
        Deletable = true,
        Help = "Setter for isKinematic of Rigidbody2D"
    )]
    public class isKinematicSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "isKinematic", Editable = true)] public Boolean iskinematic;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.isKinematic = iskinematic;
            return exit;
        }
    }
}