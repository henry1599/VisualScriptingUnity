
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddExplosionForce Overload 3",
    Path = "UnityEngine/Rigidbody/AddExplosionForce",
    Deletable = true,
    Help = "AddExplosionForce overload 3 of Rigidbody"
)]
public class AddExplosionForceNode3 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "explosionForce", Editable = true)] public Single explosionForce;
    [Input(Name = "explosionPosition", Editable = true)] public Vector3 explosionPosition;
    [Input(Name = "explosionRadius", Editable = true)] public Single explosionRadius;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        return exit;
    }
}