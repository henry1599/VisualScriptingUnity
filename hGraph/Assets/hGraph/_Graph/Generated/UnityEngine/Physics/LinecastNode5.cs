
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Linecast (Vector3 start, Vector3 end, RaycastHit hitInfo, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/Linecast",
            Deletable = true,
            Help = "Linecast overload 5 of Physics"
        )]
        public class LinecastNode5 : Node
        {
            [Input] public Node entry;
            [Input(Name = "start", Editable = true)] public Vector3 start;
    [Input(Name = "end", Editable = true)] public Vector3 end;
    [Input(Name = "hitInfo", Editable = true)] public RaycastHit hitInfo;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.Linecast(start, end, out hitInfo, layerMask);
                return result;
            }
        }
    }