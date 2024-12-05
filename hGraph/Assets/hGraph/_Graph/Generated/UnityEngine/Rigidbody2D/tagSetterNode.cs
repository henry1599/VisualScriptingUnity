
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/tag",
        Deletable = true,
        Help = "Setter for tag of Rigidbody2D"
    )]
    public class tagSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "tag", Editable = true)] public String tag;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.tag = tag;
            return exit;
        }
    }
}