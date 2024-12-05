
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/gameObject",
        Deletable = true,
        Help = "Getter for gameObject of Rigidbody2D"
    )]
    public class gameObjectGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "gameObject")] public GameObject gameobject;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.gameObject;
        }
    }
}