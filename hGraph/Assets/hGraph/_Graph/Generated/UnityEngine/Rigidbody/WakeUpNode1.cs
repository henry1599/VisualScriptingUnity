
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "WakeUp Overload 1",
    Path = "UnityEngine/Rigidbody/WakeUp",
    Deletable = true,
    Help = "WakeUp overload 1 of Rigidbody"
)]
public class WakeUpNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.WakeUp();
        return exit;
    }
}