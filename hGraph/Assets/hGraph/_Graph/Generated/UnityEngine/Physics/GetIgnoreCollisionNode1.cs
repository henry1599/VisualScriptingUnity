
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) GetIgnoreCollision (Collider collider1, Collider collider2)",
            Path = "UnityEngine/Physics/Methods/GetIgnoreCollision",
            Deletable = true,
            Help = "GetIgnoreCollision overload 1 of Physics"
        )]
        public class GetIgnoreCollisionNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "collider1", Editable = true)] public Collider collider1;
    [Input(Name = "collider2", Editable = true)] public Collider collider2;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.GetIgnoreCollision(collider1, collider2);
                return result;
            }
        }
    }