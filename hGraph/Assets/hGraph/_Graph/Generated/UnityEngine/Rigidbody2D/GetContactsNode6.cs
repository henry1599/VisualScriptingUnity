
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetContacts (List<Collider2D> colliders)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetContacts",
            Deletable = true,
            Help = "GetContacts overload 6 of Rigidbody2D"
        )]
        public class GetContactsNode6 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "colliders", Editable = true)] public List<Collider2D> colliders;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.GetContacts(colliders);
                return result;
            }
        }
    }