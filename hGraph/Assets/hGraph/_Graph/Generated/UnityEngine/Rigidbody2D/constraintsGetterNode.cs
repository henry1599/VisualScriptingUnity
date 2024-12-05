
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/constraints",
        Deletable = true,
        Help = "Getter for constraints of Rigidbody2D"
    )]
    public class constraintsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "constraints")] public RigidbodyConstraints2D constraints;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.constraints;
        }
    }
}