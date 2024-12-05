
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
{
    [Node(
        Name = "Getter",
        Path = "System.Text/StringBuilder/Properties/Capacity",
        Deletable = true,
        Help = "Getter for Capacity of StringBuilder"
    )]
    public class CapacityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
        [Output(Name = "Capacity")] public Int32 capacity;

        public override object OnRequestValue(Port port)
        {
            return stringbuilder.Capacity;
        }
    }
}