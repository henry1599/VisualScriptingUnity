
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastNonAlloc (Ray ray, Single radius, RaycastHit[] results)",
            Path = "UnityEngine/Physics/Methods/SphereCastNonAlloc",
            Deletable = true,
            Help = "SphereCastNonAlloc overload 8 of Physics"
        )]
        public class SphereCastNonAllocNode8 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.SphereCastNonAlloc(ray, radius, results);
                return result;
            }
        }
    }