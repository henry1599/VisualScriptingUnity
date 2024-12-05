
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/tag",
        Deletable = true,
        Help = "Setter for tag of Rigidbody"
    )]
    public class tagSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "tag", Editable = true)] public String tag;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.tag = tag;
            return exit;
        }
    }
}