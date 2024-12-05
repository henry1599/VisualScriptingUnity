
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/excludeLayers",
        Deletable = true,
        Help = "Getter for excludeLayers of BoxCollider"
    )]
    public class excludeLayersGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "excludeLayers")] public LayerMask excludelayers;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.excludeLayers;
        }
    }
}