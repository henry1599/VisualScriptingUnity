
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapCapsuleNonAlloc (Vector3 point0, Vector3 point1, Single radius, Collider[] results, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/OverlapCapsuleNonAlloc",
            Deletable = true,
            Help = "OverlapCapsuleNonAlloc overload 2 of Physics"
        )]
        public class OverlapCapsuleNonAllocNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "point0", Editable = true)] public Vector3 point0;
    [Input(Name = "point1", Editable = true)] public Vector3 point1;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "results", Editable = true)] public Collider[] results;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask);
                return result;
            }
        }
    }