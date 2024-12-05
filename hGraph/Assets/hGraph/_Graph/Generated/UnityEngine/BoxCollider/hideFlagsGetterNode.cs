
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/hideFlags",
        Deletable = true,
        Help = "Getter for hideFlags of BoxCollider"
    )]
    public class hideFlagsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "hideFlags")] public HideFlags hideflags;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.hideFlags;
        }
    }
}