
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
    namespace CustomNode.UnityEngine.Rigidbody_Generated
    {
        [Node(
            Name = "CompareTag (String tag)",
            Path = "UnityEngine/Rigidbody/Methods/CompareTag",
            Deletable = true,
            Help = "CompareTag overload 1 of Rigidbody"
        )]
        public class CompareTagNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody")] public Rigidbody rigidbody;
            [Input(Name = "tag", Editable = true)] public String tag;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody.CompareTag(tag);
                return result;
            }
        }
    }