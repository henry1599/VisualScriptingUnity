
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{
    [Node(
        Name = "Getter",
        Path = "UnityEngine/Rigidbody2D/Properties/sleepMode",
        Deletable = true,
        Help = "Getter for sleepMode of Rigidbody2D"
    )]
    public class sleepModeGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Output(Name = "sleepMode")] public RigidbodySleepMode2D sleepmode;

        public override object OnRequestValue(Port port)
        {
            return rigidbody2d.sleepMode;
        }
    }
}