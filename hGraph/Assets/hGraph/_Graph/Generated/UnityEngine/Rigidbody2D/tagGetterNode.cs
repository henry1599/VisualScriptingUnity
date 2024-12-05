
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/tag",
        Deletable = true,
        Help = "Getter for tag of Rigidbody2D"
    )]
    public class tagGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "tag")] public String tag;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.tag;
        }
    }
}