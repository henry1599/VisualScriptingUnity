
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/BoxCollider/Properties/center",
        Deletable = true,
        Help = "Setter for center of BoxCollider"
    )]
    public class centerSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
        [Input(Name = "center", Editable = true)] public Vector3 center;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            boxcollider.center = center;
            return exit;
        }
    }
}