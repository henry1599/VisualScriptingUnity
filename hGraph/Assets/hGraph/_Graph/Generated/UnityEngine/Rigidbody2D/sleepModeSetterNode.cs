
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
{

    [Node(
        Name = "Setter",
        Path = "UnityEngine/Rigidbody2D/Properties/sleepMode",
        Deletable = true,
        Help = "Setter for sleepMode of Rigidbody2D"
    )]
    public class sleepModeSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
        [Input(Name = "sleepMode", Editable = true)] public RigidbodySleepMode2D sleepmode;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            rigidbody2d.sleepMode = sleepmode;
            return exit;
        }
    }
}