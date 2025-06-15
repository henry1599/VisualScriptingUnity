using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class SaveToolbarItem : ToolbarItem
    {
        public void OnSaveButtonClicked()
        {
            PopupManager.Instance?.PushPopup(new ShowPopupArg(ePopupType.Save_Your_Work));
        }
    }
}
