
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Raycast (Ray ray, RaycastHit hitInfo, Single maxDistance)",
            Path = "UnityEngine/Physics/Methods/Raycast",
            Deletable = true,
            Help = "Raycast overload 15 of Physics"
        )]
        public class RaycastNode15 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.Raycast(ray, out hitInfo, maxDistance);
                return result;
            }
        }
    }