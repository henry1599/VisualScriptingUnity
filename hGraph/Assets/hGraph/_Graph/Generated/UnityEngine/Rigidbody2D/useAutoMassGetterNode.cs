
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/useAutoMass",
        Deletable = true,
        Help = "Getter for useAutoMass of Rigidbody2D"
    )]
    public class useAutoMassGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "useAutoMass")] public Boolean useautomass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.useAutoMass;
        }
    }
}