
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/transform",
        Deletable = true,
        Help = "Getter for transform of BoxCollider"
    )]
    public class transformGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "transform")] public Transform transform;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.transform;
        }
    }
}