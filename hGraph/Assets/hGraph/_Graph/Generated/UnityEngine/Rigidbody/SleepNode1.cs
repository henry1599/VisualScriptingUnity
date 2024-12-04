
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "Sleep Overload 1",
    Path = "UnityEngine/Rigidbody/Sleep",
    Deletable = true,
    Help = "Sleep overload 1 of Rigidbody"
)]
public class SleepNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.Sleep();
        return exit;
    }
}