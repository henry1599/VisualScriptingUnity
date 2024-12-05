
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) BoxCastNonAlloc (Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)",
            Path = "UnityEngine/Physics/Methods/BoxCastNonAlloc",
            Deletable = true,
            Help = "BoxCastNonAlloc overload 5 of Physics"
        )]
        public class BoxCastNonAllocNode5 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.BoxCastNonAlloc(center, halfExtents, direction, results);
                return result;
            }
        }
    }