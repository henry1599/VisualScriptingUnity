
    using BlueGraph;
using Object = UnityEngine.Object;
using System.Text;
using System;
    namespace CustomNode.System.Text.StringBuilder_Generated
    {
        [Node(
            Name = "AppendLine",
            Path = "System.Text/StringBuilder/Methods",
            Deletable = true,
            Help = "AppendLine overload 1 of StringBuilder"
        )]
        public class AppendLineNode1 : Node
        {
            [Input] public Node entry;
            [Input(Name = "StringBuilder")] public StringBuilder stringbuilder;
            

            [Output(Name = "result")] public StringBuilder result;

            public override object OnRequestValue(Port port)
            {
                StringBuilder result = stringbuilder.AppendLine();
                return result;
            }
        }
    }