
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/simulated",
        Deletable = true,
        Help = "Getter for simulated of Rigidbody2D"
    )]
    public class simulatedGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "simulated")] public Boolean simulated;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.simulated;
        }
    }
}