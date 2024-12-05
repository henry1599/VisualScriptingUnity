
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/collisionDetectionMode",
        Deletable = true,
        Help = "Getter for collisionDetectionMode of Rigidbody"
    )]
    public class collisionDetectionModeGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "collisionDetectionMode")] public CollisionDetectionMode collisiondetectionmode;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.collisionDetectionMode;
        }
    }
}