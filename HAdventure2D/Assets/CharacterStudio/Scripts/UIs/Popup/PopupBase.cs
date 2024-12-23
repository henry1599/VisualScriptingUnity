using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public abstract class PopupBase : MonoBehaviour, IPopup
    {
        [SerializeField] Button _cancelButton;
        public abstract ePopupType PopupType { get; }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
            gameObject.SetActive(true);
        }

        private void OnCancelButtonClicked()
        {
            EventBus.Instance.Publish(new HidePopupArg(PopupType));
        }
    }
}
