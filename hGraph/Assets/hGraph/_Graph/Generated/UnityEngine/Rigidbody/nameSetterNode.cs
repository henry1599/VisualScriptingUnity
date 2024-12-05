
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/name",
        Deletable = true,
        Help = "Setter for name of Rigidbody"
    )]
    public class nameSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "name", Editable = true)] public String name;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.name = name;
            return exit;
        }
    }
}