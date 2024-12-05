
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/rotation",
        Deletable = true,
        Help = "Getter for rotation of Rigidbody2D"
    )]
    public class rotationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "rotation")] public Single rotation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.rotation;
        }
    }
}