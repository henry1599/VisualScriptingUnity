using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
