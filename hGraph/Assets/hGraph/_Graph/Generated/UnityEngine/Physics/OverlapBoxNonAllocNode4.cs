
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapBoxNonAlloc (Vector3 center, Vector3 halfExtents, Collider[] results)",
            Path = "UnityEngine/Physics/Methods/OverlapBoxNonAlloc",
            Deletable = true,
            Help = "OverlapBoxNonAlloc overload 4 of Physics"
        )]
        public class OverlapBoxNonAllocNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "results", Editable = true)] public Collider[] results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.OverlapBoxNonAlloc(center, halfExtents, results);
                return result;
            }
        }
    }