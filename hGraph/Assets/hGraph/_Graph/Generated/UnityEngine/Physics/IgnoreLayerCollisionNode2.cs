
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) IgnoreLayerCollision (Int32 layer1, Int32 layer2)",
            Path = "UnityEngine/Physics/Methods/IgnoreLayerCollision",
            Deletable = true,
            Help = "IgnoreLayerCollision overload 2 of Physics"
        )]
        public class IgnoreLayerCollisionNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "layer1", Editable = true)] public Int32 layer1;
    [Input(Name = "layer2", Editable = true)] public Int32 layer2;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.IgnoreLayerCollision(layer1, layer2);
                return exit;
            }
        }
    }