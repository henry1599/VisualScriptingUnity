
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CheckBox (Vector3 center, Vector3 halfExtents, Quaternion orientation, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/CheckBox",
            Deletable = true,
            Help = "CheckBox overload 2 of Physics"
        )]
        public class CheckBoxNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "center", Editable = true)] public Vector3 center;
    [Input(Name = "halfExtents", Editable = true)] public Vector3 halfExtents;
    [Input(Name = "orientation", Editable = true)] public Quaternion orientation;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.CheckBox(center, halfExtents, orientation, layerMask);
                return result;
            }
        }
    }