using System;

namespace CharacterStudio
{
    public class HidePopupArg : EventArgs
    {
        public ePopupType PopupType { get; private set; }
        public HidePopupArg(ePopupType popupType)
        {
            PopupType = popupType;
        }
    }
}
