
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/contactOffset",
        Deletable = true,
        Help = "Getter for contactOffset of BoxCollider"
    )]
    public class contactOffsetGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "contactOffset")] public Single contactoffset;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.contactOffset;
        }
    }
}