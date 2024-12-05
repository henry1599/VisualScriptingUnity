
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/name",
        Deletable = true,
        Help = "Setter for name of BoxCollider"
    )]
    public class nameSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "name", Editable = true)] public String name;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.name = name;
            return exit;
        }
    }
}