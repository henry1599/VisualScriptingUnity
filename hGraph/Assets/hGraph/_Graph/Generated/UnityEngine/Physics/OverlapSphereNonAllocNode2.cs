
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapSphereNonAlloc (Vector3 position, Single radius, Collider[] results, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/OverlapSphereNonAlloc",
            Deletable = true,
            Help = "OverlapSphereNonAlloc overload 2 of Physics"
        )]
        public class OverlapSphereNonAllocNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "position", Editable = true)] public Vector3 position;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "results", Editable = true)] public Collider[] results;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
                return result;
            }
        }
    }