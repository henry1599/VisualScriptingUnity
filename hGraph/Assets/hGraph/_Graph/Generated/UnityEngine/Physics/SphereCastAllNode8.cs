
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCastAll (Ray ray, Single radius)",
            Path = "UnityEngine/Physics/Methods/SphereCastAll",
            Deletable = true,
            Help = "SphereCastAll overload 8 of Physics"
        )]
        public class SphereCastAllNode8 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.SphereCastAll(ray, radius);
                return result;
            }
        }
    }