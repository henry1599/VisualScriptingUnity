
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/tag",
        Deletable = true,
        Help = "Getter for tag of Rigidbody"
    )]
    public class tagGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "tag")] public String tag;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.tag;
        }
    }
}