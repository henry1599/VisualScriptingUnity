
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/sharedMaterial",
        Deletable = true,
        Help = "Getter for sharedMaterial of BoxCollider"
    )]
    public class sharedMaterialGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "sharedMaterial")] public PhysicMaterial sharedmaterial;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.sharedMaterial;
        }
    }
}