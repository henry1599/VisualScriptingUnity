
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/position",
        Deletable = true,
        Help = "Getter for position of Rigidbody"
    )]
    public class positionGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "position")] public Vector3 position;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.position;
        }
    }
}