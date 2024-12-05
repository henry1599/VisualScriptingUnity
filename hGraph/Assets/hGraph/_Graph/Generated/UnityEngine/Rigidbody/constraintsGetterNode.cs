
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/constraints",
        Deletable = true,
        Help = "Getter for constraints of Rigidbody"
    )]
    public class constraintsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "constraints")] public RigidbodyConstraints constraints;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.constraints;
        }
    }
}