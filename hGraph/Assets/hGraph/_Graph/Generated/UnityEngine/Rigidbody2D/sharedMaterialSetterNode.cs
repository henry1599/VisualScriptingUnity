
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/sharedMaterial",
        Deletable = true,
        Help = "Setter for sharedMaterial of Rigidbody2D"
    )]
    public class sharedMaterialSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "sharedMaterial", Editable = true)] public PhysicsMaterial2D sharedmaterial;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.sharedMaterial = sharedmaterial;
            return exit;
        }
    }
}