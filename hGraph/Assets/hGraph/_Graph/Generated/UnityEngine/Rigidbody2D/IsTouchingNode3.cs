
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "IsTouching (ContactFilter2D contactFilter)",
            Path = "UnityEngine/Rigidbody2D/Methods/IsTouching",
            Deletable = true,
            Help = "IsTouching overload 3 of Rigidbody2D"
        )]
        public class IsTouchingNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "contactFilter", Editable = true)] public ContactFilter2D contactFilter;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.IsTouching(contactFilter);
                return result;
            }
        }
    }