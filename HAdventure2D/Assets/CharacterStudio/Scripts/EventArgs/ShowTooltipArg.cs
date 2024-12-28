using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
