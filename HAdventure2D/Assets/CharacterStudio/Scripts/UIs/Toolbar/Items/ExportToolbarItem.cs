using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class ExportToolbarItem : ToolbarItem
    {
        public void OnSpriteSheetButtonClicked()
        {

        }
        public void OnSpritesButtonClicked()
        {
            PopupManager.Instance?.PushPopup(new ShowPopupArg(ePopupType.ExportSeparatedSprites));
        }
        public void OnSpriteLibraryButtonClicked()
        {

        }
        public void OnAllButtonClicked()
        {

        }
    }
}
