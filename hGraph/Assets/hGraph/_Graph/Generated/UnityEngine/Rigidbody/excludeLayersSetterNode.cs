
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/excludeLayers",
        Deletable = true,
        Help = "Setter for excludeLayers of Rigidbody"
    )]
    public class excludeLayersSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "excludeLayers", Editable = true)] public LayerMask excludelayers;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.excludeLayers = excludelayers;
            return exit;
        }
    }
}