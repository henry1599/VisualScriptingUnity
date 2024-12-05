
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/solverIterations",
        Deletable = true,
        Help = "Setter for solverIterations of Rigidbody"
    )]
    public class solverIterationsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "solverIterations", Editable = true)] public Int32 solveriterations;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.solverIterations = solveriterations;
            return exit;
        }
    }
}