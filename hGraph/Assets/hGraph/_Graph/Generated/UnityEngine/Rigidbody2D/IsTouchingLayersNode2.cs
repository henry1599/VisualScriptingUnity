
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "IsTouchingLayers (Int32 layerMask)",
            Path = "UnityEngine/Rigidbody2D/Methods/IsTouchingLayers",
            Deletable = true,
            Help = "IsTouchingLayers overload 2 of Rigidbody2D"
        )]
        public class IsTouchingLayersNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.IsTouchingLayers(layerMask);
                return result;
            }
        }
    }