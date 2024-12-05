
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/excludeLayers",
        Deletable = true,
        Help = "Setter for excludeLayers of Rigidbody2D"
    )]
    public class excludeLayersSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "excludeLayers", Editable = true)] public LayerMask excludelayers;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.excludeLayers = excludelayers;
            return exit;
        }
    }
}