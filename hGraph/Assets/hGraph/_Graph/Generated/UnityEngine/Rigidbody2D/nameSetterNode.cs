
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/name",
        Deletable = true,
        Help = "Setter for name of Rigidbody2D"
    )]
    public class nameSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "name", Editable = true)] public String name;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.name = name;
            return exit;
        }
    }
}