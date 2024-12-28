using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterStudio
{
    [System.Serializable]
    public class ToolbarContextMenuItem
    {
        public string Text;
        public TooltipData Tooltip; 
        public UnityEvent OnClick;
    }
}
