
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/velocity",
        Deletable = true,
        Help = "Getter for velocity of Rigidbody"
    )]
    public class velocityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "velocity")] public Vector3 velocity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.velocity;
        }
    }
}