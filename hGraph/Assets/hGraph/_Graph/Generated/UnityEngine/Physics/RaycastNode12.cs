
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Raycast (Ray ray)",
            Path = "UnityEngine/Physics/Methods/Raycast",
            Deletable = true,
            Help = "Raycast overload 12 of Physics"
        )]
        public class RaycastNode12 : Node
        {
            [Input] public Node entry;
            [Input(Name = "ray", Editable = true)] public Ray ray;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.Raycast(ray);
                return result;
            }
        }
    }