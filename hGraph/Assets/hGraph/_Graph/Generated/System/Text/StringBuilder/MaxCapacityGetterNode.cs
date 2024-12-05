
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
{
    [Node(
        Name = "Getter",
        Path = "System.Text/StringBuilder/Properties/MaxCapacity",
        Deletable = true,
        Help = "Getter for MaxCapacity of StringBuilder"
    )]
    public class MaxCapacityGetterNode : Node
    {
        [Input] public Node entry;
        [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
        [Output(Name = "MaxCapacity")] public Int32 maxcapacity;

        public override object OnRequestValue(Port port)
        {
            return stringbuilder.MaxCapacity;
        }
    }
}