
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/drag",
        Deletable = true,
        Help = "Getter for drag of Rigidbody2D"
    )]
    public class dragGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "drag")] public Single drag;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.drag;
        }
    }
}