
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/bounds",
        Deletable = true,
        Help = "Getter for bounds of BoxCollider"
    )]
    public class boundsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "bounds")] public Bounds bounds;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.bounds;
        }
    }
}