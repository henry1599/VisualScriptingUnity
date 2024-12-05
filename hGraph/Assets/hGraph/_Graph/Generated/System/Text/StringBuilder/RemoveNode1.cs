
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "Remove (Int32 startIndex, Int32 length)",
            Path = "System.Text/StringBuilder/Methods/Remove",
            Deletable = true,
            Help = "Remove overload 1 of StringBuilder"
        )]
        public class RemoveNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "startIndex", Editable = true)] public Int32 startIndex;
    [Input(Name = "length", Editable = true)] public Int32 length;

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.Remove(startIndex, length);
                return result;
            }
        }
    }