
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/constraints",
        Deletable = true,
        Help = "Setter for constraints of Rigidbody"
    )]
    public class constraintsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "constraints", Editable = true)] public RigidbodyConstraints constraints;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.constraints = constraints;
            return exit;
        }
    }
}