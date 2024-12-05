
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "Cast (Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, Single distance)",
            Path = "UnityEngine/Rigidbody2D/Methods/Cast",
            Deletable = true,
            Help = "Cast overload 6 of Rigidbody2D"
        )]
        public class CastNode6 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "direction", Editable = true)] public Vector2 direction;
    [Input(Name = "contactFilter", Editable = true)] public ContactFilter2D contactFilter;
    [Input(Name = "results", Editable = true)] public List<RaycastHit2D> results;
    [Input(Name = "distance", Editable = true)] public Single distance;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.Cast(direction, contactFilter, results, distance);
                return result;
            }
        }
    }