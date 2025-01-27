using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSToolbar_SaveButton : ToolbarItem
    {
        public void OnSaveButtonClicked()
        {
            EventBus.Instance.Publish(new SavePaintingArg());
            EventBus.Instance.Publish(new ShowPopupArg(ePopupType.CSP_SaveSuccess));
        }
    }

    public class SavePaintingArg : EventArgs
    {
        public SavePaintingArg() { }
    }
}
