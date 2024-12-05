
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/velocity",
        Deletable = true,
        Help = "Setter for velocity of Rigidbody"
    )]
    public class velocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "velocity", Editable = true)] public Vector3 velocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.velocity = velocity;
            return exit;
        }
    }
}