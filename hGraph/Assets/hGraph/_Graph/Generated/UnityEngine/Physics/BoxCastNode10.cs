
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) BoxCast (Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit hitInfo)",
            Path = "UnityEngine/Physics/Methods/BoxCast",
            Deletable = true,
            Help = "BoxCast overload 10 of Physics"
        )]
        public class BoxCastNode10 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.BoxCast(center, halfExtents, direction, out hitInfo);
                return result;
            }
        }
    }