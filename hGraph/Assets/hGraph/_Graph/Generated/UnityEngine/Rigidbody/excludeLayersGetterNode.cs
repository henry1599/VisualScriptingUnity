
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/excludeLayers",
        Deletable = true,
        Help = "Getter for excludeLayers of Rigidbody"
    )]
    public class excludeLayersGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "excludeLayers")] public LayerMask excludelayers;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.excludeLayers;
        }
    }
}