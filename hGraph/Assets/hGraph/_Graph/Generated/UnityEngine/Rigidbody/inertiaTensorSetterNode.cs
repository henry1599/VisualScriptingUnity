
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/inertiaTensor",
        Deletable = true,
        Help = "Setter for inertiaTensor of Rigidbody"
    )]
    public class inertiaTensorSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "inertiaTensor", Editable = true)] public Vector3 inertiatensor;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.inertiaTensor = inertiatensor;
            return exit;
        }
    }
}