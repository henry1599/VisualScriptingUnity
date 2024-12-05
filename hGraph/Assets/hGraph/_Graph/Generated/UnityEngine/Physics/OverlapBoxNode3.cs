
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapBox (Vector3 center, Vector3 halfExtents, Quaternion orientation)",
            Path = "UnityEngine/Physics/Methods/OverlapBox",
            Deletable = true,
            Help = "OverlapBox overload 3 of Physics"
        )]
        public class OverlapBoxNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "orientation", Editable = true)] public Quaternion orientation;

            [Output(Name = "result")] public Collider[] result;

            public override object OnRequestValue(Port port)
            {
                Collider[] result = Physics.OverlapBox(center, halfExtents, orientation);
                return result;
            }
        }
    }