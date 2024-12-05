
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/name",
        Deletable = true,
        Help = "Getter for name of Rigidbody"
    )]
    public class nameGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "name")] public String name;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.name;
        }
    }
}