
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/centerOfMass",
        Deletable = true,
        Help = "Setter for centerOfMass of Rigidbody"
    )]
    public class centerOfMassSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "centerOfMass", Editable = true)] public Vector3 centerofmass;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.centerOfMass = centerofmass;
            return exit;
        }
    }
}