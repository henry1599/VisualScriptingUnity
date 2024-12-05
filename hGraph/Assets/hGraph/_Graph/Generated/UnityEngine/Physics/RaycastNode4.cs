
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Raycast (Vector3 origin, Vector3 direction)",
            Path = "UnityEngine/Physics/Methods/Raycast",
            Deletable = true,
            Help = "Raycast overload 4 of Physics"
        )]
        public class RaycastNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.Raycast(origin, direction);
                return result;
            }
        }
    }