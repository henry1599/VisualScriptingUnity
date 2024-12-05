
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) Simulate (Single step)",
            Path = "UnityEngine/Physics/Methods/Simulate",
            Deletable = true,
            Help = "Simulate overload 1 of Physics"
        )]
        public class SimulateNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "step", Editable = true)] public Single step;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.Simulate(step);
                return exit;
            }
        }
    }