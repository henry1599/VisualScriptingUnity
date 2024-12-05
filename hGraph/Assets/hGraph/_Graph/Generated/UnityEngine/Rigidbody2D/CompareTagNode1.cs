
    using BlueGraph;
using Object = UnityEngine.Object;
using UnityEngine;
using System;
using System.Collections.Generic;
    namespace CustomNode.UnityEngine.Rigidbody2D_Generated
    {
        [Node(
            Name = "CompareTag (String tag)",
            Path = "UnityEngine/Rigidbody2D/Methods/CompareTag",
            Deletable = true,
            Help = "CompareTag overload 1 of Rigidbody2D"
        )]
        public class CompareTagNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "Rigidbody2D")] public Rigidbody2D rigidbody2d;
            [Input(Name = "tag", Editable = true)] public String tag;

            [Output(Name = "result")] public Boolean result;

            public override object OnRequestValue(Port port)
            {
                Boolean result = rigidbody2d.CompareTag(tag);
                return result;
            }
        }
    }