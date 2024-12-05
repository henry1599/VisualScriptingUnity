
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RaycastAll (Ray ray)",
            Path = "UnityEngine/Physics/Methods/RaycastAll",
            Deletable = true,
            Help = "RaycastAll overload 8 of Physics"
        )]
        public class RaycastAllNode8 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.RaycastAll(ray);
                return result;
            }
        }
    }