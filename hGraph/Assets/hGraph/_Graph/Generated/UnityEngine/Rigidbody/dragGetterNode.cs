
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/drag",
        Deletable = true,
        Help = "Getter for drag of Rigidbody"
    )]
    public class dragGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "drag")] public Single drag;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.drag;
        }
    }
}