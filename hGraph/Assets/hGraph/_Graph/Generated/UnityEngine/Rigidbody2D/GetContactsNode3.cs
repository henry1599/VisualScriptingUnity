
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetContacts (ContactFilter2D contactFilter, ContactPoint2D[] contacts)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetContacts",
            Deletable = true,
            Help = "GetContacts overload 3 of Rigidbody2D"
        )]
        public class GetContactsNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "contactFilter", Editable = true)] public ContactFilter2D contactFilter;
    [Input(Name = "contacts", Editable = true)] public ContactPoint2D[] contacts;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.GetContacts(contactFilter, contacts);
                return result;
            }
        }
    }