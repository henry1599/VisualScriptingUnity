
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/enabled",
        Deletable = true,
        Help = "Setter for enabled of BoxCollider"
    )]
    public class enabledSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "enabled", Editable = true)] public Boolean enabled;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.enabled = enabled;
            return exit;
        }
    }
}