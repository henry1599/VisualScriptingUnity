
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Append (Char[] value, Int32 startIndex, Int32 charCount)",
            Path = "System.Text/StringBuilder/Methods/Append",
            Deletable = true,
            Help = "Append overload 2 of StringBuilder"
        )]
        public class AppendNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "value", Editable = true)] public Char[] value;
    [Input(Name = "startIndex", Editable = true)] public Int32 startIndex;
    [Input(Name = "charCount", Editable = true)] public Int32 charCount;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Append(value, startIndex, charCount);
                return result;
            }
        }
    }