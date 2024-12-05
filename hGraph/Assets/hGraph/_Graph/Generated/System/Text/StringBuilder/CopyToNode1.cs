
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "CopyTo (Int32 sourceIndex, Char[] destination, Int32 destinationIndex, Int32 count)",
            Path = "System.Text/StringBuilder/Methods/CopyTo",
            Deletable = true,
            Help = "CopyTo overload 1 of StringBuilder"
        )]
        public class CopyToNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            [Input(Name = "sourceIndex", Editable = true)] public Int32 sourceIndex;
    [Input(Name = "destination", Editable = true)] public Char[] destination;
    [Input(Name = "destinationIndex", Editable = true)] public Int32 destinationIndex;
    [Input(Name = "count", Editable = true)] public Int32 count;

            [Output] public Node exit;

            public override object OnRequestValue(Port port)
            {
                stringbuilder.CopyTo(sourceIndex, destination, destinationIndex, count);
                return exit;
            }
        }
    }