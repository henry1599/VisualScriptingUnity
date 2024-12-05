
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/maxDepenetrationVelocity",
        Deletable = true,
        Help = "Setter for maxDepenetrationVelocity of Rigidbody"
    )]
    public class maxDepenetrationVelocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "maxDepenetrationVelocity", Editable = true)] public Single maxdepenetrationvelocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.maxDepenetrationVelocity = maxdepenetrationvelocity;
            return exit;
        }
    }
}