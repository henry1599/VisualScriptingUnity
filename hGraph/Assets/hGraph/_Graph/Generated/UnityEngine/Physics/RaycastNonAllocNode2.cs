
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RaycastNonAlloc (Ray ray, RaycastHit[] results, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/RaycastNonAlloc",
            Deletable = true,
            Help = "RaycastNonAlloc overload 2 of Physics"
        )]
        public class RaycastNonAllocNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask);
                return result;
            }
        }
    }