
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) SyncTransforms",
            Path = "UnityEngine/Physics/Methods",
            Deletable = true,
            Help = "SyncTransforms overload 1 of Physics"
        )]
        public class SyncTransformsNode1 : Node
        {
            [Input] public Node entry;
            

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.SyncTransforms();
                return exit;
            }
        }
    }