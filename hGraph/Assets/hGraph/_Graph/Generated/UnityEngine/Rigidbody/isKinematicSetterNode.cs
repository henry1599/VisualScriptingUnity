
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/isKinematic",
        Deletable = true,
        Help = "Setter for isKinematic of Rigidbody"
    )]
    public class isKinematicSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "isKinematic", Editable = true)] public Boolean iskinematic;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.isKinematic = iskinematic;
            return exit;
        }
    }
}