
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/includeLayers",
        Deletable = true,
        Help = "Getter for includeLayers of Rigidbody"
    )]
    public class includeLayersGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "includeLayers")] public LayerMask includelayers;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.includeLayers;
        }
    }
}