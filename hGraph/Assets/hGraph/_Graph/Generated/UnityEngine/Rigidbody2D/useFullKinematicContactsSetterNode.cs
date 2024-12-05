
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/useFullKinematicContacts",
        Deletable = true,
        Help = "Setter for useFullKinematicContacts of Rigidbody2D"
    )]
    public class useFullKinematicContactsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "useFullKinematicContacts", Editable = true)] public Boolean usefullkinematiccontacts;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.useFullKinematicContacts = usefullkinematiccontacts;
            return exit;
        }
    }
}