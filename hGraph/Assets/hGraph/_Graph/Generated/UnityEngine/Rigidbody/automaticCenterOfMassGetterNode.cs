
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/automaticCenterOfMass",
        Deletable = true,
        Help = "Getter for automaticCenterOfMass of Rigidbody"
    )]
    public class automaticCenterOfMassGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "automaticCenterOfMass")] public Boolean automaticcenterofmass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.automaticCenterOfMass;
        }
    }
}