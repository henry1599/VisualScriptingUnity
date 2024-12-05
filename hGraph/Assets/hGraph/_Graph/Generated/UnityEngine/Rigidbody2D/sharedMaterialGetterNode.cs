
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/sharedMaterial",
        Deletable = true,
        Help = "Getter for sharedMaterial of Rigidbody2D"
    )]
    public class sharedMaterialGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "sharedMaterial")] public PhysicsMaterial2D sharedmaterial;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.sharedMaterial;
        }
    }
}