using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class ExportToolbarItem : ToolbarItem
    {
        public void OnSpriteSheetButtonClicked()
        {
            PopupManager.Instance?.PushPopup( new ShowPopupArg( ePopupType.ExportSpriteSheet ) );
        }
        public void OnSpritesButtonClicked()
        {
            PopupManager.Instance?.PushPopup(new ShowPopupArg(ePopupType.ExportSeparatedSprites));
        }
        public void OnSpriteLibraryButtonClicked()
        {
            PopupManager.Instance?.PushPopup(new ShowPopupArg(ePopupType.ExportSpriteLibrary));
        }
        public void OnAllButtonClicked()
        {

        }
    }
}
