
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/enabled",
        Deletable = true,
        Help = "Getter for enabled of BoxCollider"
    )]
    public class enabledGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "enabled")] public Boolean enabled;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.enabled;
        }
    }
}