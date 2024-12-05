
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/contactOffset",
        Deletable = true,
        Help = "Setter for contactOffset of BoxCollider"
    )]
    public class contactOffsetSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "contactOffset", Editable = true)] public Single contactoffset;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.contactOffset = contactoffset;
            return exit;
        }
    }
}