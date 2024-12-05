
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/gravityScale",
        Deletable = true,
        Help = "Setter for gravityScale of Rigidbody2D"
    )]
    public class gravityScaleSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "gravityScale", Editable = true)] public Single gravityscale;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.gravityScale = gravityscale;
            return exit;
        }
    }
}