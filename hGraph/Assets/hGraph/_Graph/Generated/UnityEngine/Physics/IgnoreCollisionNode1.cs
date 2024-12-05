
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) IgnoreCollision (Collider collider1, Collider collider2, Boolean ignore)",
            Path = "UnityEngine/Physics/Methods/IgnoreCollision",
            Deletable = true,
            Help = "IgnoreCollision overload 1 of Physics"
        )]
        public class IgnoreCollisionNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "collider1", Editable = true)] public Collider collider1;
    [Input(Name = "collider2", Editable = true)] public Collider collider2;
    [Input(Name = "ignore", Editable = true)] public Boolean ignore;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.IgnoreCollision(collider1, collider2, ignore);
                return exit;
            }
        }
    }