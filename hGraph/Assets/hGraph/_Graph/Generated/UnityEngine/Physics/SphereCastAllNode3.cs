
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastAll (Vector3 origin, Single radius, Vector3 direction, Single maxDistance)",
            Path = "UnityEngine/Physics/Methods/SphereCastAll",
            Deletable = true,
            Help = "SphereCastAll overload 3 of Physics"
        )]
        public class SphereCastAllNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.SphereCastAll(origin, radius, direction, maxDistance);
                return result;
            }
        }
    }