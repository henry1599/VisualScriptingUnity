
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
{

    [Node(
        Name = "Setter",
        Path = "System.Text/StringBuilder/Properties/Length",
        Deletable = true,
        Help = "Setter for Length of StringBuilder"
    )]
    public class LengthSetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
        [Input(Name = "Length", Editable = true)] public Int32 length;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            stringbuilder.Length = length;
            return exit;
        }
    }
}