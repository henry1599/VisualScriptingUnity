
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastAll (Ray ray, Single radius, Single maxDistance)",
            Path = "UnityEngine/Physics/Methods/SphereCastAll",
            Deletable = true,
            Help = "SphereCastAll overload 7 of Physics"
        )]
        public class SphereCastAllNode7 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.SphereCastAll(ray, radius, maxDistance);
                return result;
            }
        }
    }