
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/tag",
        Deletable = true,
        Help = "Getter for tag of BoxCollider"
    )]
    public class tagGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "tag")] public String tag;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.tag;
        }
    }
}