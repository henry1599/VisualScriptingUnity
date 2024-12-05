
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/simulated",
        Deletable = true,
        Help = "Setter for simulated of Rigidbody2D"
    )]
    public class simulatedSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "simulated", Editable = true)] public Boolean simulated;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.simulated = simulated;
            return exit;
        }
    }
}