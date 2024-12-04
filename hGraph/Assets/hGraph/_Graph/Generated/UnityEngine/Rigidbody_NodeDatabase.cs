using BlueGraph;
using UnityEngine;

[Node(
    Name = "Velocity", 
    Path = "UnityEngine/Rigidbody/Velocity/Get", 
    Deletable = true
)]
public class Velocity_Get : Node
{
    [Input] public Rigidbody rigidbody;
    [Output] public Vector3 velocity;

    public override object OnRequestValue(Port port)
    {
        return rigidbody.velocity;
    }
}


[Node(
    Name = "Velocity", 
    Path = "UnityEngine/Rigidbody/Velocity/Set", 
    Deletable = true
)]
public class Velocity_Set : Node
{
    [Output] public Node exit;
    [Input] public Rigidbody rigidbody;

    public override object OnRequestValue(Port port)
    {
        rigidbody.velocity = port.GetValue<Vector3>();
        return this;
    }
}