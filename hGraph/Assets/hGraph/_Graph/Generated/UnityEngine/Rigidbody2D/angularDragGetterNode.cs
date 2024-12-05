
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/angularDrag",
        Deletable = true,
        Help = "Getter for angularDrag of Rigidbody2D"
    )]
    public class angularDragGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "angularDrag")] public Single angulardrag;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.angularDrag;
        }
    }
}