
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/attachedColliderCount",
        Deletable = true,
        Help = "Getter for attachedColliderCount of Rigidbody2D"
    )]
    public class attachedColliderCountGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "attachedColliderCount")] public Int32 attachedcollidercount;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.attachedColliderCount;
        }
    }
}