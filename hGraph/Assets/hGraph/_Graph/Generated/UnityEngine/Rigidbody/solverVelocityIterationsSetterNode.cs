
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody/Properties/solverVelocityIterations",
        Deletable = true,
        Help = "Setter for solverVelocityIterations of Rigidbody"
    )]
    public class solverVelocityIterationsSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
        [Input(Name = "solverVelocityIterations", Editable = true)] public Int32 solvervelocityiterations;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody.solverVelocityIterations = solvervelocityiterations;
            return exit;
        }
    }
}