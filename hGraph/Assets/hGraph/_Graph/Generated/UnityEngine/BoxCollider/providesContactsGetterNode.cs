
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/providesContacts",
        Deletable = true,
        Help = "Getter for providesContacts of BoxCollider"
    )]
    public class providesContactsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "providesContacts")] public Boolean providescontacts;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.providesContacts;
        }
    }
}