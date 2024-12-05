
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/mass",
        Deletable = true,
        Help = "Getter for mass of Rigidbody"
    )]
    public class massGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "mass")] public Single mass;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.mass;
        }
    }
}