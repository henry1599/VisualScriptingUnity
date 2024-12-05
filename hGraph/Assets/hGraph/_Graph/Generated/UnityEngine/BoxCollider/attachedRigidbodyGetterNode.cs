
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/attachedRigidbody",
        Deletable = true,
        Help = "Getter for attachedRigidbody of BoxCollider"
    )]
    public class attachedRigidbodyGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "attachedRigidbody")] public Rigidbody attachedrigidbody;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.attachedRigidbody;
        }
    }
}