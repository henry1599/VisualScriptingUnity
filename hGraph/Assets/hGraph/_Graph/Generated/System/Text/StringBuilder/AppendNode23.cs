
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Append (Char value, Int32 valueCount)",
            Path = "System.Text/StringBuilder/Methods/Append",
            Deletable = true,
            Help = "Append overload 23 of StringBuilder"
        )]
        public class AppendNode23 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "value", Editable = true)] public Char value;
    [Input(Name = "valueCount", Editable = true)] public Int32 valueCount;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Append(value, valueCount);
                return result;
            }
        }
    }