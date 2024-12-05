
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/automaticInertiaTensor",
        Deletable = true,
        Help = "Getter for automaticInertiaTensor of Rigidbody"
    )]
    public class automaticInertiaTensorGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "automaticInertiaTensor")] public Boolean automaticinertiatensor;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.automaticInertiaTensor;
        }
    }
}