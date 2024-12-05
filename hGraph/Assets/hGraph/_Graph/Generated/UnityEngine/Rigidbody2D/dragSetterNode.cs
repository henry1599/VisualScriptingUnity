
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/drag",
        Deletable = true,
        Help = "Setter for drag of Rigidbody2D"
    )]
    public class dragSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "drag", Editable = true)] public Single drag;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.drag = drag;
            return exit;
        }
    }
}