
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/hideFlags",
        Deletable = true,
        Help = "Setter for hideFlags of BoxCollider"
    )]
    public class hideFlagsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "hideFlags", Editable = true)] public HideFlags hideflags;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.hideFlags = hideflags;
            return exit;
        }
    }
}