
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/angularVelocity",
        Deletable = true,
        Help = "Setter for angularVelocity of Rigidbody"
    )]
    public class angularVelocitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "angularVelocity", Editable = true)] public Vector3 angularvelocity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.angularVelocity = angularvelocity;
            return exit;
        }
    }
}