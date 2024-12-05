
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/includeLayers",
        Deletable = true,
        Help = "Setter for includeLayers of Rigidbody"
    )]
    public class includeLayersSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "includeLayers", Editable = true)] public LayerMask includelayers;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.includeLayers = includelayers;
            return exit;
        }
    }
}