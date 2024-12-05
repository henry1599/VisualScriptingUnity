
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/useGravity",
        Deletable = true,
        Help = "Setter for useGravity of Rigidbody"
    )]
    public class useGravitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "useGravity", Editable = true)] public Boolean usegravity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.useGravity = usegravity;
            return exit;
        }
    }
}