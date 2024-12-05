
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/useAutoMass",
        Deletable = true,
        Help = "Setter for useAutoMass of Rigidbody2D"
    )]
    public class useAutoMassSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "useAutoMass", Editable = true)] public Boolean useautomass;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.useAutoMass = useautomass;
            return exit;
        }
    }
}