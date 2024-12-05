
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/mass",
        Deletable = true,
        Help = "Setter for mass of Rigidbody2D"
    )]
    public class massSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "mass", Editable = true)] public Single mass;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.mass = mass;
            return exit;
        }
    }
}