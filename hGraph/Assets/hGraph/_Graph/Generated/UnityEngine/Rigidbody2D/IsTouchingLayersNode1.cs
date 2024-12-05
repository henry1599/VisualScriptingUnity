
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "IsTouchingLayers",
            Path = "UnityEngine/Rigidbody2D/Methods",
            Deletable = true,
            Help = "IsTouchingLayers overload 1 of Rigidbody2D"
        )]
        public class IsTouchingLayersNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.IsTouchingLayers();
                return result;
            }
        }
    }