
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SphereCast (Ray ray, Single radius)",
            Path = "UnityEngine/Physics/Methods/SphereCast",
            Deletable = true,
            Help = "SphereCast overload 8 of Physics"
        )]
        public class SphereCastNode8 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "radius", Editable = true)] public Single radius;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.SphereCast(ray, radius);
                return result;
            }
        }
    }