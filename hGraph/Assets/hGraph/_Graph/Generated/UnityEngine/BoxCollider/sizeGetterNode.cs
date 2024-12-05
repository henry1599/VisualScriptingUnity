
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/BoxCollider/Properties/size",
        Deletable = true,
        Help = "Getter for size of BoxCollider"
    )]
    public class sizeGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Output(Name = "size")] public Vector3 size;

        public override object OnRequestValue(Port port)
        {
            return boxcollider.size;
        }
    }
}