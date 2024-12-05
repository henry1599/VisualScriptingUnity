
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/hideFlags",
        Deletable = true,
        Help = "Getter for hideFlags of Rigidbody"
    )]
    public class hideFlagsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "hideFlags")] public HideFlags hideflags;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.hideFlags;
        }
    }
}