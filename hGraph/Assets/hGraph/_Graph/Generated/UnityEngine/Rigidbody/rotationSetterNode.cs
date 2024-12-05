
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/rotation",
        Deletable = true,
        Help = "Setter for rotation of Rigidbody"
    )]
    public class rotationSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "rotation", Editable = true)] public Quaternion rotation;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.rotation = rotation;
            return exit;
        }
    }
}