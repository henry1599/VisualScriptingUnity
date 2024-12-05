
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/size",
        Deletable = true,
        Help = "Setter for size of BoxCollider"
    )]
    public class sizeSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "size", Editable = true)] public Vector3 size;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.size = size;
            return exit;
        }
    }
}