
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/gameObject",
        Deletable = true,
        Help = "Getter for gameObject of BoxCollider"
    )]
    public class gameObjectGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "gameObject")] public GameObject gameobject;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.gameObject;
        }
    }
}