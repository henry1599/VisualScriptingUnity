
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/material",
        Deletable = true,
        Help = "Getter for material of BoxCollider"
    )]
    public class materialGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "material")] public PhysicMaterial material;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.material;
        }
    }
}