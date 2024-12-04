
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "ToString Overload 1",
    Path = "UnityEngine/Rigidbody/ToString",
    Deletable = true,
    Help = "ToString overload 1 of Rigidbody"
)]
public class ToStringNode1 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    

    [Output(Name = "result")] public String result;

    public override object OnRequestValue(Port port)
    {
        String result = rigidbody.ToString();
        return result;
    }
}