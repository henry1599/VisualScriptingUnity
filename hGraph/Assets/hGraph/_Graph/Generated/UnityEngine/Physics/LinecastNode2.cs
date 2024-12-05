
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Linecast (Vector3 start, Vector3 end, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/Linecast",
            Deletable = true,
            Help = "Linecast overload 2 of Physics"
        )]
        public class LinecastNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "start", Editable = true)] public Vector3 start;
    [Input(Name = "end", Editable = true)] public Vector3 end;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.Linecast(start, end, layerMask);
                return result;
            }
        }
    }