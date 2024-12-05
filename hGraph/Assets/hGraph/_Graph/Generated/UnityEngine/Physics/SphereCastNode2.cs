
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCast (Vector3 origin, Single radius, Vector3 direction, RaycastHit hitInfo, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/SphereCast",
            Deletable = true,
            Help = "SphereCast overload 2 of Physics"
        )]
        public class SphereCastNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask);
                return result;
            }
        }
    }