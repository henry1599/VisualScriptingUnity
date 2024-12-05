
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/gravityScale",
        Deletable = true,
        Help = "Getter for gravityScale of Rigidbody2D"
    )]
    public class gravityScaleGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "gravityScale")] public Single gravityscale;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.gravityScale;
        }
    }
}