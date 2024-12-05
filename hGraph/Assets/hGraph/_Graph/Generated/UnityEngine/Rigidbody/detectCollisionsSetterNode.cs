
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/detectCollisions",
        Deletable = true,
        Help = "Setter for detectCollisions of Rigidbody"
    )]
    public class detectCollisionsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "detectCollisions", Editable = true)] public Boolean detectcollisions;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.detectCollisions = detectcollisions;
            return exit;
        }
    }
}