
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/includeLayers",
        Deletable = true,
        Help = "Setter for includeLayers of Rigidbody2D"
    )]
    public class includeLayersSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "includeLayers", Editable = true)] public LayerMask includelayers;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.includeLayers = includelayers;
            return exit;
        }
    }
}