
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "IsTouching (Collider2D collider, ContactFilter2D contactFilter)",
            Path = "UnityEngine/Rigidbody2D/Methods/IsTouching",
            Deletable = true,
            Help = "IsTouching overload 2 of Rigidbody2D"
        )]
        public class IsTouchingNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "collider", Editable = true)] public Collider2D collider;
    [Input(Name = "contactFilter", Editable = true)] public ContactFilter2D contactFilter;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.IsTouching(collider, contactFilter);
                return result;
            }
        }
    }