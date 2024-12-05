
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/mass",
        Deletable = true,
        Help = "Setter for mass of Rigidbody"
    )]
    public class massSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "mass", Editable = true)] public Single mass;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.mass = mass;
            return exit;
        }
    }
}