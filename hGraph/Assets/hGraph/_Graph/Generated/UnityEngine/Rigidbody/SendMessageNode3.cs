
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SendMessage Overload 3",
    Path = "UnityEngine/Rigidbody/SendMessage",
    Deletable = true,
    Help = "SendMessage overload 3 of Rigidbody"
)]
public class SendMessageNode3 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public Object value;
    [Input(Name = "options", Editable = true)] public SendMessageOptions options;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.SendMessage(methodName, value, options);
        return exit;
    }
}