
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) OverlapSphere (Vector3 position, Single radius)",
            Path = "UnityEngine/Physics/Methods/OverlapSphere",
            Deletable = true,
            Help = "OverlapSphere overload 3 of Physics"
        )]
        public class OverlapSphereNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "position", Editable = true)] public Vector3 position;
    [Input(Name = "radius", Editable = true)] public Single radius;

            [Output(Name = "result")] public Collider[] result;

            public override object OnRequestValue(Port port)
            {
                Collider[] result = Physics.OverlapSphere(position, radius);
                return result;
            }
        }
    }