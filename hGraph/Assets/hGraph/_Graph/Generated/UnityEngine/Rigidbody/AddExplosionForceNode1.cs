
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "AddExplosionForce (Single explosionForce, Vector3 explosionPosition, Single explosionRadius, Single upwardsModifier, ForceMode mode)",
            Path = "UnityEngine/Rigidbody/Methods/AddExplosionForce",
            Deletable = true,
            Help = "AddExplosionForce overload 1 of Rigidbody"
        )]
        public class AddExplosionForceNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "explosionForce", Editable = true)] public Single explosionForce;
    [Input(Name = "explosionPosition", Editable = true)] public Vector3 explosionPosition;
    [Input(Name = "explosionRadius", Editable = true)] public Single explosionRadius;
    [Input(Name = "upwardsModifier", Editable = true)] public Single upwardsModifier;
    [Input(Name = "mode", Editable = true)] public ForceMode mode;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
                return exit;
            }
        }
    }