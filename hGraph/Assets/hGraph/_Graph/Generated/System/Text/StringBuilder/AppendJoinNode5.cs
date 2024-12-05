
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
using System.Collections.Generic;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "AppendJoin (Char separator, IEnumerable<object> values)",
            Path = "System.Text/StringBuilder/Methods/AppendJoin",
            Deletable = true,
            Help = "AppendJoin overload 5 of StringBuilder"
        )]
        public class AppendJoinNode5 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "separator", Editable = true)] public Char separator;
    [Input(Name = "values", Editable = true)] public IEnumerable<object> values;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.AppendJoin(separator, values);
                return result;
            }
        }
    }