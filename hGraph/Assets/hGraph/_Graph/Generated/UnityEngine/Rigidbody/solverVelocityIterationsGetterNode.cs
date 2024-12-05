
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody/Properties/solverVelocityIterations",
        Deletable = true,
        Help = "Getter for solverVelocityIterations of Rigidbody"
    )]
    public class solverVelocityIterationsGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Output(Name = "solverVelocityIterations")] public Int32 solvervelocityiterations;

        public override object OnRequestValue(Port port)
        {
            return rigidbody.solverVelocityIterations;
        }
    }
}