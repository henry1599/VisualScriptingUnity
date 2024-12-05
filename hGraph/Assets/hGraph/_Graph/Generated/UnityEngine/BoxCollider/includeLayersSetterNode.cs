
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/includeLayers",
        Deletable = true,
        Help = "Setter for includeLayers of BoxCollider"
    )]
    public class includeLayersSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "includeLayers", Editable = true)] public LayerMask includelayers;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.includeLayers = includelayers;
            return exit;
        }
    }
}