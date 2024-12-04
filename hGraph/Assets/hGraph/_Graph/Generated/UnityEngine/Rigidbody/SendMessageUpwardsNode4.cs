
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SendMessageUpwards Overload 4",
    Path = "UnityEngine/Rigidbody/SendMessageUpwards",
    Deletable = true,
    Help = "SendMessageUpwards overload 4 of Rigidbody"
)]
public class SendMessageUpwardsNode4 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.SendMessageUpwards(methodName, options);
        return exit;
    }
}