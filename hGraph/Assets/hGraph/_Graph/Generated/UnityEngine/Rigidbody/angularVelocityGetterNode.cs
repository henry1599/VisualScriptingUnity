
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/angularVelocity",
        Deletable = true,
        Help = "Getter for angularVelocity of Rigidbody"
    )]
    public class angularVelocityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "angularVelocity")] public Vector3 angularvelocity;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.angularVelocity;
        }
    }
}