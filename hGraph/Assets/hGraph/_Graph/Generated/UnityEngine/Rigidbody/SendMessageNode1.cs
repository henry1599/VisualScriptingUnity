
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "SendMessage Overload 1",
    Path = "UnityEngine/Rigidbody/SendMessage",
    Deletable = true,
    Help = "SendMessage overload 1 of Rigidbody"
)]
public class SendMessageNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "methodName", Editable = true)] public String methodName;
    [Input(Name = "value", Editable = true)] public Object value;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.SendMessage(methodName, value);
        return exit;
    }
}