using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class PopupSaveSuccess : PopupBase
    {
        public override ePopupType PopupType => ePopupType.CSP_SaveSuccess;
        public override void Show()
        {
            base.Show();
        }
    }
}
