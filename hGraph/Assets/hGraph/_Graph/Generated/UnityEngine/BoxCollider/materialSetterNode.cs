
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/material",
        Deletable = true,
        Help = "Setter for material of BoxCollider"
    )]
    public class materialSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "material", Editable = true)] public PhysicMaterial material;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.material = material;
            return exit;
        }
    }
}