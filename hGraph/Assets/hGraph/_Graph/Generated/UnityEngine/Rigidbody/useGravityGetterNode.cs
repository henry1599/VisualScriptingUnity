
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/useGravity",
        Deletable = true,
        Help = "Getter for useGravity of Rigidbody"
    )]
    public class useGravityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "useGravity")] public Boolean usegravity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.useGravity;
        }
    }
}