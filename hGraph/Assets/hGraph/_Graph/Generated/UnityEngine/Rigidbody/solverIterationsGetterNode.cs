
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/solverIterations",
        Deletable = true,
        Help = "Getter for solverIterations of Rigidbody"
    )]
    public class solverIterationsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "solverIterations")] public Int32 solveriterations;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.solverIterations;
        }
    }
}