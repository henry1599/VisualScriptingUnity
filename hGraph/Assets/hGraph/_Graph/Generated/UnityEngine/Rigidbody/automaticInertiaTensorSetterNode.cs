
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/automaticInertiaTensor",
        Deletable = true,
        Help = "Setter for automaticInertiaTensor of Rigidbody"
    )]
    public class automaticInertiaTensorSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "automaticInertiaTensor", Editable = true)] public Boolean automaticinertiatensor;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.automaticInertiaTensor = automaticinertiatensor;
            return exit;
        }
    }
}