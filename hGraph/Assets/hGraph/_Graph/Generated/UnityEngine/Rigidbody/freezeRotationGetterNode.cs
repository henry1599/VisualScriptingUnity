
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/freezeRotation",
        Deletable = true,
        Help = "Getter for freezeRotation of Rigidbody"
    )]
    public class freezeRotationGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "freezeRotation")] public Boolean freezerotation;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.freezeRotation;
        }
    }
}