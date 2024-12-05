
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/excludeLayers",
        Deletable = true,
        Help = "Getter for excludeLayers of Rigidbody2D"
    )]
    public class excludeLayersGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "excludeLayers")] public LayerMask excludelayers;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.excludeLayers;
        }
    }
}