
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Linecast (Vector3 start, Vector3 end)",
            Path = "UnityEngine/Physics/Methods/Linecast",
            Deletable = true,
            Help = "Linecast overload 3 of Physics"
        )]
        public class LinecastNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "start", Editable = true)] public Vector3 start;
    [Input(Name = "end", Editable = true)] public Vector3 end;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.Linecast(start, end);
                return result;
            }
        }
    }