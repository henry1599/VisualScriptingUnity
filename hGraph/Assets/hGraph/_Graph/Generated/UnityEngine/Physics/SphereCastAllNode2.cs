
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastAll (Vector3 origin, Single radius, Vector3 direction, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/SphereCastAll",
            Deletable = true,
            Help = "SphereCastAll overload 2 of Physics"
        )]
        public class SphereCastAllNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask);
                return result;
            }
        }
    }