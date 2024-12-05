
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/transform",
        Deletable = true,
        Help = "Getter for transform of Rigidbody2D"
    )]
    public class transformGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "transform")] public Transform transform;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.transform;
        }
    }
}