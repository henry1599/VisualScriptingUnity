
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RaycastAll (Ray ray, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/RaycastAll",
            Deletable = true,
            Help = "RaycastAll overload 6 of Physics"
        )]
        public class RaycastAllNode6 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.RaycastAll(ray, maxDistance, layerMask);
                return result;
            }
        }
    }