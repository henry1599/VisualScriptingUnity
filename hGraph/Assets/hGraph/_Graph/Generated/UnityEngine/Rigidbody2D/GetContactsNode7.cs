
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetContacts (ContactFilter2D contactFilter, Collider2D[] colliders)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetContacts",
            Deletable = true,
            Help = "GetContacts overload 7 of Rigidbody2D"
        )]
        public class GetContactsNode7 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "contactFilter", Editable = true)] public ContactFilter2D contactFilter;
    [Input(Name = "colliders", Editable = true)] public Collider2D[] colliders;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.GetContacts(contactFilter, colliders);
                return result;
            }
        }
    }