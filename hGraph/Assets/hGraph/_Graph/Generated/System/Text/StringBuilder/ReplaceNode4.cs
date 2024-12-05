
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
using System.Collections.Generic;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Replace (Char oldChar, Char newChar, Int32 startIndex, Int32 count)",
            Path = "System.Text/StringBuilder/Methods/Replace",
            Deletable = true,
            Help = "Replace overload 4 of StringBuilder"
        )]
        public class ReplaceNode4 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "oldChar", Editable = true)] public Char oldChar;
    [Input(Name = "newChar", Editable = true)] public Char newChar;
    [Input(Name = "startIndex", Editable = true)] public Int32 startIndex;
    [Input(Name = "count", Editable = true)] public Int32 count;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Replace(oldChar, newChar, startIndex, count);
                return result;
            }
        }
    }