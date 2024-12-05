
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastNonAlloc (Ray ray, Single radius, RaycastHit[] results, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/SphereCastNonAlloc",
            Deletable = true,
            Help = "SphereCastNonAlloc overload 6 of Physics"
        )]
        public class SphereCastNonAllocNode6 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask);
                return result;
            }
        }
    }