
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
using System.Collections.Generic;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "AppendFormat (String format, object arg0)",
            Path = "System.Text/StringBuilder/Methods/AppendFormat",
            Deletable = true,
            Help = "AppendFormat overload 1 of StringBuilder"
        )]
        public class AppendFormatNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "format", Editable = true)] public String format;
    [Input(Name = "arg0", Editable = true)] public object arg0;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.AppendFormat(format, arg0);
                return result;
            }
        }
    }