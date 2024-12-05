
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/position",
        Deletable = true,
        Help = "Setter for position of Rigidbody2D"
    )]
    public class positionSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "position", Editable = true)] public Vector2 position;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.position = position;
            return exit;
        }
    }
}