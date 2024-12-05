
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.BoxCollider_Generated
    {
        [Node(
            Name = "CompareTag (String tag)",
            Path = "UnityEngine/BoxCollider/Methods/CompareTag",
            Deletable = true,
            Help = "CompareTag overload 1 of BoxCollider"
        )]
        public class CompareTagNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "BoxCollider")] public BoxCollider boxcollider;
            [Input(Name = "tag", Editable = true)] public String tag;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = boxcollider.CompareTag(tag);
                return result;
            }
        }
    }