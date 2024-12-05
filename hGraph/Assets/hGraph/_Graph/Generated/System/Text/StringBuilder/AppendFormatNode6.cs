
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
using System.Collections.Generic;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "AppendFormat (IFormatProvider provider, String format, object arg0, object arg1)",
            Path = "System.Text/StringBuilder/Methods/AppendFormat",
            Deletable = true,
            Help = "AppendFormat overload 6 of StringBuilder"
        )]
        public class AppendFormatNode6 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "provider", Editable = true)] public IFormatProvider provider;
    [Input(Name = "format", Editable = true)] public String format;
    [Input(Name = "arg0", Editable = true)] public object arg0;
    [Input(Name = "arg1", Editable = true)] public object arg1;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.AppendFormat(provider, format, arg0, arg1);
                return result;
            }
        }
    }