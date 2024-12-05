
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/freezeRotation",
        Deletable = true,
        Help = "Setter for freezeRotation of Rigidbody"
    )]
    public class freezeRotationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "freezeRotation", Editable = true)] public Boolean freezerotation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.freezeRotation = freezerotation;
            return exit;
        }
    }
}