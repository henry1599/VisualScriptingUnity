
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "GetShapes (PhysicsShapeGroup2D physicsShapeGroup)",
            Path = "UnityEngine/Rigidbody2D/Methods/GetShapes",
            Deletable = true,
            Help = "GetShapes overload 1 of Rigidbody2D"
        )]
        public class GetShapesNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "physicsShapeGroup", Editable = true)] public PhysicsShapeGroup2D physicsShapeGroup;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = rigidbody2d.GetShapes(physicsShapeGroup);
                return result;
            }
        }
    }