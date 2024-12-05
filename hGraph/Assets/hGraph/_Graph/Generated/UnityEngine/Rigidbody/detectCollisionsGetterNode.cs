
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/detectCollisions",
        Deletable = true,
        Help = "Getter for detectCollisions of Rigidbody"
    )]
    public class detectCollisionsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "detectCollisions")] public Boolean detectcollisions;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.detectCollisions;
        }
    }
}