
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/centerOfMass",
        Deletable = true,
        Help = "Getter for centerOfMass of Rigidbody2D"
    )]
    public class centerOfMassGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "centerOfMass")] public Vector2 centerofmass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.centerOfMass;
        }
    }
}