
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Insert (Int32 index, String value)",
            Path = "System.Text/StringBuilder/Methods/Insert",
            Deletable = true,
            Help = "Insert overload 19 of StringBuilder"
        )]
        public class InsertNode19 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "index", Editable = true)] public Int32 index;
    [Input(Name = "value", Editable = true)] public String value;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Insert(index, value.ToCharArray());
                return result;
            }
        }
    }