
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/attachedArticulationBody",
        Deletable = true,
        Help = "Getter for attachedArticulationBody of BoxCollider"
    )]
    public class attachedArticulationBodyGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "attachedArticulationBody")] public ArticulationBody attachedarticulationbody;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.attachedArticulationBody;
        }
    }
}