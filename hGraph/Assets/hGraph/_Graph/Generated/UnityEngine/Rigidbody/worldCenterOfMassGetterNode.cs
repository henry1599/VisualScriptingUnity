
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/worldCenterOfMass",
        Deletable = true,
        Help = "Getter for worldCenterOfMass of Rigidbody"
    )]
    public class worldCenterOfMassGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "worldCenterOfMass")] public Vector3 worldcenterofmass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.worldCenterOfMass;
        }
    }
}