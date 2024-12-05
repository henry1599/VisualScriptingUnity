
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Insert (Int32 index, Char value)",
            Path = "System.Text/StringBuilder/Methods/Insert",
            Deletable = true,
            Help = "Insert overload 7 of StringBuilder"
        )]
        public class InsertNode7 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "index", Editable = true)] public Int32 index;
    [Input(Name = "value", Editable = true)] public Char value;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Insert(index, value);
                return result;
            }
        }
    }