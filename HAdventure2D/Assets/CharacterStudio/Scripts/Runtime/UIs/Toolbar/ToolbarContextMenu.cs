using UnityEngine.Events;

namespace CharacterStudio
{
    [System.Serializable]
    public class ToolbarContextMenuItem
    {
        public bool EditorExclusive = false;
        public string EditorExclusiveMessage = "Unity Editor only";
        public string EditorExclusiveMessageTooltip = "This feature can only work in Unity Editor";
        public string Text;
        public TooltipData Tooltip;
        public UnityEvent OnClick;
    }
}
