
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/position",
        Deletable = true,
        Help = "Setter for position of Rigidbody"
    )]
    public class positionSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "position", Editable = true)] public Vector3 position;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.position = position;
            return exit;
        }
    }
}