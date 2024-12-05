
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/centerOfMass",
        Deletable = true,
        Help = "Getter for centerOfMass of Rigidbody"
    )]
    public class centerOfMassGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "centerOfMass")] public Vector3 centerofmass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.centerOfMass;
        }
    }
}