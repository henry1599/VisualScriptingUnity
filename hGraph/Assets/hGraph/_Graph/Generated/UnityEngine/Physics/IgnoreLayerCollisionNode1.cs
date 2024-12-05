
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) IgnoreLayerCollision (Int32 layer1, Int32 layer2, Boolean ignore)",
            Path = "UnityEngine/Physics/Methods/IgnoreLayerCollision",
            Deletable = true,
            Help = "IgnoreLayerCollision overload 1 of Physics"
        )]
        public class IgnoreLayerCollisionNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "layer1", Editable = true)] public Int32 layer1;
    [Input(Name = "layer2", Editable = true)] public Int32 layer2;
    [Input(Name = "ignore", Editable = true)] public Boolean ignore;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                Physics.IgnoreLayerCollision(layer1, layer2, ignore);
                return exit;
            }
        }
    }