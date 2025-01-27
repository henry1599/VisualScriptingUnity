using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public enum ePopupType
    {
        None,
        ExportSeparatedSprites,
        ExportSpriteSheet,
        ExportSpriteLibrary,
        CSP_Save,
        CSP_ConfirmBack,
        CSP_SaveSuccess,
    }
    public interface IPopup
    {
        ePopupType PopupType { get; }
        void Show();
        void Hide();
    }
}
