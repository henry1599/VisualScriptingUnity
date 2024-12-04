
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SendMessage Overload 4",
    Path = "UnityEngine/Rigidbody/SendMessage",
    Deletable = true,
    Help = "SendMessage overload 4 of Rigidbody"
)]
public class SendMessageNode4 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.SendMessage(methodName, options);
        return exit;
    }
}