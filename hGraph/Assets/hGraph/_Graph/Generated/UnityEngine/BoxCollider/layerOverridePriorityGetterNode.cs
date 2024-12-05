
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/layerOverridePriority",
        Deletable = true,
        Help = "Getter for layerOverridePriority of BoxCollider"
    )]
    public class layerOverridePriorityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "layerOverridePriority")] public Int32 layeroverridepriority;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.layerOverridePriority;
        }
    }
}