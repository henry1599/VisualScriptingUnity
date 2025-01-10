using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
