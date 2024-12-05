
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/transform",
        Deletable = true,
        Help = "Getter for transform of Rigidbody"
    )]
    public class transformGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "transform")] public Transform transform;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.transform;
        }
    }
}