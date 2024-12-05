
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/includeLayers",
        Deletable = true,
        Help = "Getter for includeLayers of Rigidbody2D"
    )]
    public class includeLayersGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "includeLayers")] public LayerMask includelayers;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.includeLayers;
        }
    }
}