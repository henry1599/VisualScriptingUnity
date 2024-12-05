
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/center",
        Deletable = true,
        Help = "Getter for center of BoxCollider"
    )]
    public class centerGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "center")] public Vector3 center;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.center;
        }
    }
}