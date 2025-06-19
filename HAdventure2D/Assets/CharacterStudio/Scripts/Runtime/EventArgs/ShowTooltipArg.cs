using System;

namespace CharacterStudio
{
    public class ShowTooltipArg : EventArgs
    {
        public TooltipData Data { get; private set; }
        public ShowTooltipArg(TooltipData data)
        {
            Data = data;
        }
    }
}
