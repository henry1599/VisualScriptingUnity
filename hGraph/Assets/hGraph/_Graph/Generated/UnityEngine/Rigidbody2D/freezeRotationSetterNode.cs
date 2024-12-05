
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/freezeRotation",
        Deletable = true,
        Help = "Setter for freezeRotation of Rigidbody2D"
    )]
    public class freezeRotationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "freezeRotation", Editable = true)] public Boolean freezerotation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.freezeRotation = freezerotation;
            return exit;
        }
    }
}