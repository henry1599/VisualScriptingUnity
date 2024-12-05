
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/totalTorque",
        Deletable = true,
        Help = "Setter for totalTorque of Rigidbody2D"
    )]
    public class totalTorqueSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "totalTorque", Editable = true)] public Single totaltorque;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.totalTorque = totaltorque;
            return exit;
        }
    }
}