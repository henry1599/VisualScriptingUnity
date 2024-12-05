
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
{
    [Node(
        Name = "Getter",
        Path = "System.Text/StringBuilder/Properties/Length",
        Deletable = true,
        Help = "Getter for Length of StringBuilder"
    )]
    public class LengthGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
        [Output(Name = "Length")] public Int32 length;

        public override object OnRequestValue(Port port)
        {
            return stringbuilder.Length;
        }
    }
}