
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/hasModifiableContacts",
        Deletable = true,
        Help = "Setter for hasModifiableContacts of BoxCollider"
    )]
    public class hasModifiableContactsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "hasModifiableContacts", Editable = true)] public Boolean hasmodifiablecontacts;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.hasModifiableContacts = hasmodifiablecontacts;
            return exit;
        }
    }
}