
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "EnsureCapacity (Int32 capacity)",
            Path = "System.Text/StringBuilder/Methods/EnsureCapacity",
            Deletable = true,
            Help = "EnsureCapacity overload 1 of StringBuilder"
        )]
        public class EnsureCapacityNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "capacity", Editable = true)] public Int32 capacity;

            [Output(Name = "result")] public Int32 result;

            public override object OnRequestValue(Port port)
            {
                Int32 result = stringbuilder.EnsureCapacity(capacity);
                return result;
            }
        }
    }