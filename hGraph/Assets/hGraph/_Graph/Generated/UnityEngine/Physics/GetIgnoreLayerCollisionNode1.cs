
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Physics_Generated
    {
        [Node(
            Name = "(Static) GetIgnoreLayerCollision (Int32 layer1, Int32 layer2)",
            Path = "UnityEngine/Physics/Methods/GetIgnoreLayerCollision",
            Deletable = true,
            Help = "GetIgnoreLayerCollision overload 1 of Physics"
        )]
        public class GetIgnoreLayerCollisionNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "layer1", Editable = true)] public Int32 layer1;
    [Input(Name = "layer2", Editable = true)] public Int32 layer2;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = Physics.GetIgnoreLayerCollision(layer1, layer2);
                return result;
            }
        }
    }