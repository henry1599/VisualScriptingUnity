
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "AppendJoin (String separator, object[] values)",
            Path = "System.Text/StringBuilder/Methods/AppendJoin",
            Deletable = true,
            Help = "AppendJoin overload 1 of StringBuilder"
        )]
        public class AppendJoinNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "separator", Editable = true)] public String separator;
    [Input(Name = "values", Editable = true)] public object[] values;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.AppendJoin(separator, values);
                return result;
            }
        }
    }