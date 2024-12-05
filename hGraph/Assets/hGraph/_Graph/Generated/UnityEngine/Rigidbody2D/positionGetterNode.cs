
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/position",
        Deletable = true,
        Help = "Getter for position of Rigidbody2D"
    )]
    public class positionGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "position")] public Vector2 position;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.position;
        }
    }
}