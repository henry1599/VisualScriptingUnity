
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/gameObject",
        Deletable = true,
        Help = "Getter for gameObject of Rigidbody"
    )]
    public class gameObjectGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "gameObject")] public GameObject gameobject;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.gameObject;
        }
    }
}