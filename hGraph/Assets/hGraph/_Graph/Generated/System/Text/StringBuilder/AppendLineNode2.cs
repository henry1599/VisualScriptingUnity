
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "AppendLine (String value)",
            Path = "System.Text/StringBuilder/Methods/AppendLine",
            Deletable = true,
            Help = "AppendLine overload 2 of StringBuilder"
        )]
        public class AppendLineNode2 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "value", Editable = true)] public String value;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.AppendLine(value);
                return result;
            }
        }
    }