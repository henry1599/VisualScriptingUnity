
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CheckCapsule (Vector3 start, Vector3 end, Single radius)",
            Path = "UnityEngine/Physics/Methods/CheckCapsule",
            Deletable = true,
            Help = "CheckCapsule overload 3 of Physics"
        )]
        public class CheckCapsuleNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "start", Editable = true)] public Vector3 start;
    [Input(Name = "end", Editable = true)] public Vector3 end;
    [Input(Name = "radius", Editable = true)] public Single radius;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.CheckCapsule(start, end, radius);
                return result;
            }
        }
    }