
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) ComputePenetration (Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, Vector3 direction, Single distance)",
            Path = "UnityEngine/Physics/Methods/ComputePenetration",
            Deletable = true,
            Help = "ComputePenetration overload 1 of Physics"
        )]
        public class ComputePenetrationNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "colliderA", Editable = true)] public Collider colliderA;
    [Input(Name = "positionA", Editable = true)] public Vector3 positionA;
    [Input(Name = "rotationA", Editable = true)] public Quaternion rotationA;
    [Input(Name = "colliderB", Editable = true)] public Collider colliderB;
    [Input(Name = "positionB", Editable = true)] public Vector3 positionB;
    [Input(Name = "rotationB", Editable = true)] public Quaternion rotationB;
    [Input(Name = "direction", Editable = true)] public Vector3 direction;
    [Input(Name = "distance", Editable = true)] public Single distance;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction, out distance);
                return result;
            }
        }
    }