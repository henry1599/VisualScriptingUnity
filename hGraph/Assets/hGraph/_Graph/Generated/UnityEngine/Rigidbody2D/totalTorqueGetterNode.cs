
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/totalTorque",
        Deletable = true,
        Help = "Getter for totalTorque of Rigidbody2D"
    )]
    public class totalTorqueGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "totalTorque")] public Single totaltorque;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.totalTorque;
        }
    }
}