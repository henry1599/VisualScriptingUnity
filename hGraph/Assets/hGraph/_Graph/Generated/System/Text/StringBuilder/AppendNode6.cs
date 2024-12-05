
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Append (StringBuilder value, Int32 startIndex, Int32 count)",
            Path = "System.Text/StringBuilder/Methods/Append",
            Deletable = true,
            Help = "Append overload 6 of StringBuilder"
        )]
        public class AppendNode6 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "value", Editable = true)] public StringBuilder value;
    [Input(Name = "startIndex", Editable = true)] public Int32 startIndex;
    [Input(Name = "count", Editable = true)] public Int32 count;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Append(value, startIndex, count);
                return result;
            }
        }
    }