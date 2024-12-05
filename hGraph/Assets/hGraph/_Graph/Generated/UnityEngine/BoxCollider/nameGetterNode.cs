
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/name",
        Deletable = true,
        Help = "Getter for name of BoxCollider"
    )]
    public class nameGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "name")] public String name;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.name;
        }
    }
}