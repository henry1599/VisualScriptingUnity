
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RaycastNonAlloc (Vector3 origin, Vector3 direction, RaycastHit[] results)",
            Path = "UnityEngine/Physics/Methods/RaycastNonAlloc",
            Deletable = true,
            Help = "RaycastNonAlloc overload 8 of Physics"
        )]
        public class RaycastNonAllocNode8 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "results", Editable = true)] public RaycastHit[] results;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = Physics.RaycastNonAlloc(origin, direction, results);
                return result;
            }
        }
    }