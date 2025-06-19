using System;

namespace CharacterStudio
{
    public class ShowPopupArg : EventArgs
    {
        public ePopupType PopupType { get; private set; }
        public ShowPopupArg(ePopupType popupType)
        {
            PopupType = popupType;
        }
    }
}
