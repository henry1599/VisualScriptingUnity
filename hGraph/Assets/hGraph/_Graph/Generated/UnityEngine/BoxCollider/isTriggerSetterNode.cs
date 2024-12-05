
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/isTrigger",
        Deletable = true,
        Help = "Setter for isTrigger of BoxCollider"
    )]
    public class isTriggerSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "isTrigger", Editable = true)] public Boolean istrigger;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.isTrigger = istrigger;
            return exit;
        }
    }
}