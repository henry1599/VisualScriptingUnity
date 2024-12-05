
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) BoxCast (Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, Single maxDistance, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/BoxCast",
            Deletable = true,
            Help = "BoxCast overload 2 of Physics"
        )]
        public class BoxCastNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "orientation", Editable = true)] public Quaternion orientation;
    [Input(Name = "maxDistance", Editable = true)] public Single maxDistance;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask);
                return result;
            }
        }
    }