
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
using System.Collections.Generic;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Replace (String oldValue, String newValue, Int32 startIndex, Int32 count)",
            Path = "System.Text/StringBuilder/Methods/Replace",
            Deletable = true,
            Help = "Replace overload 2 of StringBuilder"
        )]
        public class ReplaceNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "oldValue", Editable = true)] public String oldValue;
    [Input(Name = "newValue", Editable = true)] public String newValue;
    [Input(Name = "startIndex", Editable = true)] public Int32 startIndex;
    [Input(Name = "count", Editable = true)] public Int32 count;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Replace(oldValue, newValue, startIndex, count);
                return result;
            }
        }
    }