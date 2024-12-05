
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) ClosestPoint (Vector3 point, Collider collider, Vector3 position, Quaternion rotation)",
            Path = "UnityEngine/Physics/Methods/ClosestPoint",
            Deletable = true,
            Help = "ClosestPoint overload 1 of Physics"
        )]
        public class ClosestPointNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "point", Editable = true)] public Vector3 point;
    [Input(Name = "collider", Editable = true)] public Collider collider;
    [Input(Name = "position", Editable = true)] public Vector3 position;
    [Input(Name = "rotation", Editable = true)] public Quaternion rotation;

            [Output(Name = "result")] public Vector3 result;

            public override object OnRequestValue(Port port)
            {
                Vector3 result = Physics.ClosestPoint(point, collider, position, rotation);
                return result;
            }
        }
    }