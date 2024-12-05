
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) BakeMesh (Int32 meshID, Boolean convex, MeshColliderCookingOptions cookingOptions)",
            Path = "UnityEngine/Physics/Methods/BakeMesh",
            Deletable = true,
            Help = "BakeMesh overload 1 of Physics"
        )]
        public class BakeMeshNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "meshID", Editable = true)] public Int32 meshID;
    [Input(Name = "convex", Editable = true)] public Boolean convex;
    [Input(Name = "cookingOptions", Editable = true)] public MeshColliderCookingOptions cookingOptions;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.BakeMesh(meshID, convex, cookingOptions);
                return exit;
            }
        }
    }