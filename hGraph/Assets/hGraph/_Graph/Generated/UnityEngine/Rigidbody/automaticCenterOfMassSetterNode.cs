
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/automaticCenterOfMass",
        Deletable = true,
        Help = "Setter for automaticCenterOfMass of Rigidbody"
    )]
    public class automaticCenterOfMassSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "automaticCenterOfMass", Editable = true)] public Boolean automaticcenterofmass;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.automaticCenterOfMass = automaticcenterofmass;
            return exit;
        }
    }
}