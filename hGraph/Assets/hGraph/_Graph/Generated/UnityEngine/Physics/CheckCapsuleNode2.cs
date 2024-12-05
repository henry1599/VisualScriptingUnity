
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CheckCapsule (Vector3 start, Vector3 end, Single radius, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/CheckCapsule",
            Deletable = true,
            Help = "CheckCapsule overload 2 of Physics"
        )]
        public class CheckCapsuleNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "start", Editable = true)] public Vector3 start;
    [Input(Name = "end", Editable = true)] public Vector3 end;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.CheckCapsule(start, end, radius, layerMask);
                return result;
            }
        }
    }