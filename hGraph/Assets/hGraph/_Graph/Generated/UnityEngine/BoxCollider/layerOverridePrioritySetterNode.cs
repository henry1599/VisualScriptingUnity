
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/layerOverridePriority",
        Deletable = true,
        Help = "Setter for layerOverridePriority of BoxCollider"
    )]
    public class layerOverridePrioritySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "layerOverridePriority", Editable = true)] public Int32 layeroverridepriority;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.layerOverridePriority = layeroverridepriority;
            return exit;
        }
    }
}