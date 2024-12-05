
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/inertia",
        Deletable = true,
        Help = "Setter for inertia of Rigidbody2D"
    )]
    public class inertiaSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "inertia", Editable = true)] public Single inertia;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.inertia = inertia;
            return exit;
        }
    }
}