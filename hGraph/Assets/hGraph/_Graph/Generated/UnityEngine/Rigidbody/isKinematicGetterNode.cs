
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/isKinematic",
        Deletable = true,
        Help = "Getter for isKinematic of Rigidbody"
    )]
    public class isKinematicGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "isKinematic")] public Boolean iskinematic;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.isKinematic;
        }
    }
}