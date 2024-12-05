
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/hideFlags",
        Deletable = true,
        Help = "Setter for hideFlags of Rigidbody"
    )]
    public class hideFlagsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "hideFlags", Editable = true)] public HideFlags hideflags;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.hideFlags = hideflags;
            return exit;
        }
    }
}