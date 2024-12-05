
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/providesContacts",
        Deletable = true,
        Help = "Setter for providesContacts of BoxCollider"
    )]
    public class providesContactsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "providesContacts", Editable = true)] public Boolean providescontacts;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.providesContacts = providescontacts;
            return exit;
        }
    }
}