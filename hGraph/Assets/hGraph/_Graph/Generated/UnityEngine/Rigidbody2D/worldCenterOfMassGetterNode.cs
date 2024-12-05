
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/worldCenterOfMass",
        Deletable = true,
        Help = "Getter for worldCenterOfMass of Rigidbody2D"
    )]
    public class worldCenterOfMassGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "worldCenterOfMass")] public Vector2 worldcenterofmass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.worldCenterOfMass;
        }
    }
}