
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/isTrigger",
        Deletable = true,
        Help = "Getter for isTrigger of BoxCollider"
    )]
    public class isTriggerGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "isTrigger")] public Boolean istrigger;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.isTrigger;
        }
    }
}