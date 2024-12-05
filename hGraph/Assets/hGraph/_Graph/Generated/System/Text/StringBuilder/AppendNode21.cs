
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Append (Char[] value)",
            Path = "System.Text/StringBuilder/Methods/Append",
            Deletable = true,
            Help = "Append overload 21 of StringBuilder"
        )]
        public class AppendNode21 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "value", Editable = true)] public Char[] value;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Append(value);
                return result;
            }
        }
    }