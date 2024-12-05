
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/includeLayers",
        Deletable = true,
        Help = "Getter for includeLayers of BoxCollider"
    )]
    public class includeLayersGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "includeLayers")] public LayerMask includelayers;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.includeLayers;
        }
    }
}