using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PopupConfirmBack : PopupBase
    {
        public override ePopupType PopupType => ePopupType.CSP_ConfirmBack;
        [SerializeField] Button _okButton;
        void Awake()
        {
            _okButton.onClick.AddListener(OnOkButtonClicked);
        }

        private void OnOkButtonClicked()
        {
            EventBus.Instance.Publish(new OnChangeLayoutArg(eLayoutType.Character));
            EventBus.Instance.Publish(new HidePopupArg(PopupType));
        }
    }
}
