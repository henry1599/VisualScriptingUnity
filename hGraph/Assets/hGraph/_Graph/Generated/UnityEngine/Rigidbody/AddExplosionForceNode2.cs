
using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;

[Node(
    Name = "AddExplosionForce Overload 2",
    Path = "UnityEngine/Rigidbody/AddExplosionForce",
    Deletable = true,
    Help = "AddExplosionForce overload 2 of Rigidbody"
)]
public class AddExplosionForceNode2 : Node
{
    [Input] public Node entry;
    [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
    [Input(Name = "explosionForce", Editable = true)] public Single explosionForce;
    [Input(Name = "explosionPosition", Editable = true)] public Vector3 explosionPosition;
    [Input(Name = "explosionRadius", Editable = true)] public Single explosionRadius;
    [Input(Name = "upwardsModifier", Editable = true)] public Single upwardsModifier;

    [Output] public Node exit;

    public override object OnRequestValue(Port port)
    {
        rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier);
        return exit;
    }
}