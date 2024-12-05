
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/drag",
        Deletable = true,
        Help = "Setter for drag of Rigidbody"
    )]
    public class dragSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "drag", Editable = true)] public Single drag;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.drag = drag;
            return exit;
        }
    }
}