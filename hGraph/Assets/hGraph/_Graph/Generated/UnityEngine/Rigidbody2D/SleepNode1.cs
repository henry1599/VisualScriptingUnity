
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "Sleep",
            Path = "UnityEngine/Rigidbody2D/Methods",
            Deletable = true,
            Help = "Sleep overload 1 of Rigidbody2D"
        )]
        public class SleepNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody2d.Sleep();
                return exit;
            }
        }
    }