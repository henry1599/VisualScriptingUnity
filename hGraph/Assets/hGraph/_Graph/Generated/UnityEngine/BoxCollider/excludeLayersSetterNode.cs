
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/excludeLayers",
        Deletable = true,
        Help = "Setter for excludeLayers of BoxCollider"
    )]
    public class excludeLayersSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "excludeLayers", Editable = true)] public LayerMask excludelayers;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.excludeLayers = excludelayers;
            return exit;
        }
    }
}