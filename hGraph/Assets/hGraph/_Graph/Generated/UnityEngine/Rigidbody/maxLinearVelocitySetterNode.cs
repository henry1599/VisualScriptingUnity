
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/maxLinearVelocity",
        Deletable = true,
        Help = "Setter for maxLinearVelocity of Rigidbody"
    )]
    public class maxLinearVelocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "maxLinearVelocity", Editable = true)] public Single maxlinearvelocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.maxLinearVelocity = maxlinearvelocity;
            return exit;
        }
    }
}