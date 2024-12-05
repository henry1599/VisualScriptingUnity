
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
{

    [Node(
        Name = "Setter",
        Path = "System.Text/StringBuilder/Properties/Capacity",
        Deletable = true,
        Help = "Setter for Capacity of StringBuilder"
    )]
    public class CapacitySetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
        [Input(Name = "Capacity", Editable = true)] public Int32 capacity;
        [Output] public Node exit;

        public override object OnRequestValue(Port port)
        {
            stringbuilder.Capacity = capacity;
            return exit;
        }
    }
}