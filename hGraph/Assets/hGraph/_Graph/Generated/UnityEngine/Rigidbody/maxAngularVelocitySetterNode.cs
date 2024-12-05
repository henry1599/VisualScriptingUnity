
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/maxAngularVelocity",
        Deletable = true,
        Help = "Setter for maxAngularVelocity of Rigidbody"
    )]
    public class maxAngularVelocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "maxAngularVelocity", Editable = true)] public Single maxangularvelocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.maxAngularVelocity = maxangularvelocity;
            return exit;
        }
    }
}