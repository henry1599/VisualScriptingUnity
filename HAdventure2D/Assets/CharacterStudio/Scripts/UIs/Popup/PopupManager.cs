using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CharacterStudio
{
    public class PopupManager : MonoSingleton<PopupManager>
    {
        [SerializeField] private PopupConfig _popupConfig;
        [SerializeField] private Transform popupContainer;
        [SerializeField] private GameObject _background;
        private Stack<PopupBase> _popupStackInstance = new Stack<PopupBase>();
        private EventSubscription<ShowPopupArg> _showPopupSubscription;
        private EventSubscription<HidePopupArg> _hidePopupSubscription;

        protected override bool Awake()
        {
            _showPopupSubscription = EventBus.Instance.Subscribe<ShowPopupArg>(PushPopup);
            _hidePopupSubscription = EventBus.Instance.Subscribe<HidePopupArg>(PopPopup);

            return base.Awake();
        }
        protected override void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(_showPopupSubscription);
            EventBus.Instance.Unsubscribe(_hidePopupSubscription);
            base.OnDestroy();
        }
        public void PushPopup(ShowPopupArg arg)
        {
            GameObject popupObject = GetPopupPefab(arg.PopupType);
            if (popupObject == null)
                return;
            PopupBase popup = Instantiate(popupObject, popupContainer)?.GetComponent<PopupBase>();
            if (popup == null)
                return;
            _popupStackInstance.Push(popup);
            popup.Show();
        }
        public void PopPopup(HidePopupArg arg)
        {
            if (_popupStackInstance.Count == 0)
                return;
            PopupBase popup = _popupStackInstance.Peek();
            if (popup.PopupType != arg.PopupType)
                return;
            _popupStackInstance.Pop();
            popup.Hide();
        }
        private void Update()
        {
            _background.SetActive(_popupStackInstance.Count > 0);
        }
        GameObject GetPopupPefab(ePopupType popupType)
        {
            if (!_popupConfig.PopupPrefabs.ContainsKey(popupType))
            {
                Debug.LogError($"PopupType {popupType} is not found in PopupConfig");
                return null;
            }
            return _popupConfig.PopupPrefabs[popupType];
        }
    }
}
