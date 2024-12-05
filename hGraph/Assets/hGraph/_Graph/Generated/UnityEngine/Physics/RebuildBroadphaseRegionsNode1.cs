
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) RebuildBroadphaseRegions (Bounds worldBounds, Int32 subdivisions)",
            Path = "UnityEngine/Physics/Methods/RebuildBroadphaseRegions",
            Deletable = true,
            Help = "RebuildBroadphaseRegions overload 1 of Physics"
        )]
        public class RebuildBroadphaseRegionsNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "worldBounds", Editable = true)] public Bounds worldBounds;
    [Input(Name = "subdivisions", Editable = true)] public Int32 subdivisions;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.RebuildBroadphaseRegions(worldBounds, subdivisions);
                return exit;
            }
        }
    }