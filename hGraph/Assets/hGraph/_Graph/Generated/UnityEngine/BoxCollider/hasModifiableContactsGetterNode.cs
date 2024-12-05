
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/hasModifiableContacts",
        Deletable = true,
        Help = "Getter for hasModifiableContacts of BoxCollider"
    )]
    public class hasModifiableContactsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "hasModifiableContacts")] public Boolean hasmodifiablecontacts;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.hasModifiableContacts;
        }
    }
}