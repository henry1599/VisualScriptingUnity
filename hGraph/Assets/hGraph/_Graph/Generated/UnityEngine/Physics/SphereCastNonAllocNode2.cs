
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastNonAlloc (Vector3 origin, Single radius, Vector3 direction, RaycastHit[] results, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/SphereCastNonAlloc",
            Deletable = true,
            Help = "SphereCastNonAlloc overload 2 of Physics"
        )]
        public class SphereCastNonAllocNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask);
                return result;
            }
        }
    }