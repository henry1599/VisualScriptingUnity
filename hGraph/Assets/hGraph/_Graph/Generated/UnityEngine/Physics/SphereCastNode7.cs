
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCast (Ray ray, Single radius, Single maxDistance)",
            Path = "UnityEngine/Physics/Methods/SphereCast",
            Deletable = true,
            Help = "SphereCast overload 7 of Physics"
        )]
        public class SphereCastNode7 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.SphereCast(ray, radius, maxDistance);
                return result;
            }
        }
    }