
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CapsuleCastAll (Vector3 point1, Vector3 point2, Single radius, Vector3 direction, Single maxDistance)",
            Path = "UnityEngine/Physics/Methods/CapsuleCastAll",
            Deletable = true,
            Help = "CapsuleCastAll overload 3 of Physics"
        )]
        public class CapsuleCastAllNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "point1", Editable = true)] public Vector3 point1;
    [Input(Name = "point2", Editable = true)] public Vector3 point2;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance);
                return result;
            }
        }
    }