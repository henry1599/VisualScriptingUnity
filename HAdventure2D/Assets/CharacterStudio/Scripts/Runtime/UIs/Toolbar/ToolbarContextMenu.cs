using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterStudio
{
    [System.Serializable]
    public class ToolbarContextMenuItem
    {
        public bool EditorExclusive = false;
        [ShowIf(nameof(EditorExclusive))] public string EditorExclusiveMessage = "Unity Editor only";
        [ShowIf(nameof(EditorExclusive))] public string EditorExclusiveMessageTooltip = "This feature can only work in Unity Editor";
        public string Text;
        public TooltipData Tooltip; 
        public UnityEvent OnClick;
    }
}
