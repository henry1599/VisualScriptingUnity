
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "OverlapCollider (ContactFilter2D contactFilter, List<Collider2D> results)",
            Path = "UnityEngine/Rigidbody2D/Methods/OverlapCollider",
            Deletable = true,
            Help = "OverlapCollider overload 2 of Rigidbody2D"
        )]
        public class OverlapColliderNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "contactFilter", Editable = true)] public ContactFilter2D contactFilter;
    [Input(Name = "results", Editable = true)] public List<Collider2D> results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.OverlapCollider(contactFilter, results);
                return result;
            }
        }
    }