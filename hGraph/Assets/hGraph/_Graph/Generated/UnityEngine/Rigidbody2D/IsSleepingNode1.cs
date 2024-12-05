
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "IsSleeping",
            Path = "UnityEngine/Rigidbody2D/Methods",
            Deletable = true,
            Help = "IsSleeping overload 1 of Rigidbody2D"
        )]
        public class IsSleepingNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.IsSleeping();
                return result;
            }
        }
    }