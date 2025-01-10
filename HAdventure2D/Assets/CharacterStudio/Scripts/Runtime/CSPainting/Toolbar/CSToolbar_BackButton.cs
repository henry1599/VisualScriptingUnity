using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSToolbar_BackButton : ToolbarItem
    {
        public void OnBackButtonClicked()
        {
            PopupManager.Instance?.PushPopup(new ShowPopupArg(ePopupType.CSP_ConfirmBack));
        }
    }
}
