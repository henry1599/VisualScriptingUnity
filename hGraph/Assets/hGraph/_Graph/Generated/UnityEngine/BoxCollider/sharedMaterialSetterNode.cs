
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/sharedMaterial",
        Deletable = true,
        Help = "Setter for sharedMaterial of BoxCollider"
    )]
    public class sharedMaterialSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "sharedMaterial", Editable = true)] public PhysicMaterial sharedmaterial;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.sharedMaterial = sharedmaterial;
            return exit;
        }
    }
}