
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/rotation",
        Deletable = true,
        Help = "Setter for rotation of Rigidbody2D"
    )]
    public class rotationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "rotation", Editable = true)] public Single rotation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.rotation = rotation;
            return exit;
        }
    }
}