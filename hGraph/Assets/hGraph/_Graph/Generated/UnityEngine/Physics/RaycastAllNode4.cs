
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RaycastAll (Vector3 origin, Vector3 direction)",
            Path = "UnityEngine/Physics/Methods/RaycastAll",
            Deletable = true,
            Help = "RaycastAll overload 4 of Physics"
        )]
        public class RaycastAllNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "origin", Editable = true)] public Vector3 origin;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;

            [Output(Name = "result")] public RaycastHit[] result;

            public override object OnRequestValue(Port port)
            {
                RaycastHit[] result = Physics.RaycastAll(origin, direction);
                return result;
            }
        }
    }