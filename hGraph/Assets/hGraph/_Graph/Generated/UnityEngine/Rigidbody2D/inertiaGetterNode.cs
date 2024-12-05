
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/inertia",
        Deletable = true,
        Help = "Getter for inertia of Rigidbody2D"
    )]
    public class inertiaGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "inertia")] public Single inertia;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.inertia;
        }
    }
}