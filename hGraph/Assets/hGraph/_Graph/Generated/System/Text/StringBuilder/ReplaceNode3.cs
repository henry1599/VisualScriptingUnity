
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
using System.Collections.Generic;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Replace (Char oldChar, Char newChar)",
            Path = "System.Text/StringBuilder/Methods/Replace",
            Deletable = true,
            Help = "Replace overload 3 of StringBuilder"
        )]
        public class ReplaceNode3 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "oldChar", Editable = true)] public Char oldChar;
    [Input(Name = "newChar", Editable = true)] public Char newChar;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Replace(oldChar, newChar);
                return result;
            }
        }
    }