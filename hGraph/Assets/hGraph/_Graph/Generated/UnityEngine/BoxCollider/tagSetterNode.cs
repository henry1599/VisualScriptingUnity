
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/tag",
        Deletable = true,
        Help = "Setter for tag of BoxCollider"
    )]
    public class tagSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "tag", Editable = true)] public String tag;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.tag = tag;
            return exit;
        }
    }
}