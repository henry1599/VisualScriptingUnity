
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/collisionDetectionMode",
        Deletable = true,
        Help = "Setter for collisionDetectionMode of Rigidbody"
    )]
    public class collisionDetectionModeSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "collisionDetectionMode", Editable = true)] public CollisionDetectionMode collisiondetectionmode;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.collisionDetectionMode = collisiondetectionmode;
            return exit;
        }
    }
}