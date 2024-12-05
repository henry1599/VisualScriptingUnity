
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CapsuleCast (Vector3 point1, Vector3 point2, Single radius, Vector3 direction)",
            Path = "UnityEngine/Physics/Methods/CapsuleCast",
            Deletable = true,
            Help = "CapsuleCast overload 4 of Physics"
        )]
        public class CapsuleCastNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "point1", Editable = true)] public Vector3 point1;
    [Input(Name = "point2", Editable = true)] public Vector3 point2;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.CapsuleCast(point1, point2, radius, direction);
                return result;
            }
        }
    }