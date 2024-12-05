
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/angularDrag",
        Deletable = true,
        Help = "Setter for angularDrag of Rigidbody"
    )]
    public class angularDragSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "angularDrag", Editable = true)] public Single angulardrag;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.angularDrag = angulardrag;
            return exit;
        }
    }
}