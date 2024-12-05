
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) CheckSphere (Vector3 position, Single radius, Int32 layerMask)",
            Path = "UnityEngine/Physics/Methods/CheckSphere",
            Deletable = true,
            Help = "CheckSphere overload 2 of Physics"
        )]
        public class CheckSphereNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "position", Editable = true)] public Vector3 position;
    [Input(Name = "radius", Editable = true)] public Single radius;
    [Input(Name = "layerMask", Editable = true)] public Int32 layerMask;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.CheckSphere(position, radius, layerMask);
                return result;
            }
        }
    }