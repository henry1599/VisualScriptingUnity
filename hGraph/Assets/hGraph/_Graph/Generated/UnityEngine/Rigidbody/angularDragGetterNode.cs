
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/angularDrag",
        Deletable = true,
        Help = "Getter for angularDrag of Rigidbody"
    )]
    public class angularDragGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "angularDrag")] public Single angulardrag;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.angularDrag;
        }
    }
}