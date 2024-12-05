
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/bodyType",
        Deletable = true,
        Help = "Getter for bodyType of Rigidbody2D"
    )]
    public class bodyTypeGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "bodyType")] public RigidbodyType2D bodytype;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.bodyType;
        }
    }
}