using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSBrushUI : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] Button _button;
        [SerializeField] Image _background;
        [SerializeField] Color _selectedColor;
        [SerializeField] Color _unselectedColor;
        private CSBrush _brush;

        EventSubscription<OnBrushSelectedArgs> _brushSelectedSubscription;
        void Awake()
        {
            _brushSelectedSubscription = EventBus.Instance.Subscribe<OnBrushSelectedArgs>(OnBrushSelected);
        }
        public void Setup(CSBrush brush)
        {
            if (CSPaintingManager.Instance == null)
            {
                Debug.LogError("CSPaintingManager is not found");
                return;
            }
            Sprite brushIcon = CSPaintingManager.Instance.Setting.GetBrushIcon(brush.BrushType);
            if (brushIcon == null)
            {
                Debug.LogError("BrushIcon is not found");
                return;
            }
            var tooltipable = gameObject.SafeAddComponent<Tooltipable>();
            tooltipable.Data = brush.Tooltip;

            var shortcutable = gameObject.SafeAddComponent<Shortcutable>();
            shortcutable.Data = brush.Shortcut;
            shortcutable.Data.Callback.AddListener(SelectBrush);
            

            _icon.sprite = brushIcon;
            _background.color = _unselectedColor;
            _button.onClick.AddListener(SelectBrush);
            _brush = brush;
        }
        void OnDestroy()
        {
            _button.onClick.RemoveListener(SelectBrush);
            EventBus.Instance.Unsubscribe(_brushSelectedSubscription);
        }
        public void SelectBrush()
        {
            EventBus.Instance.Publish(new OnBrushSelectedArgs(_brush.BrushType));
        }
        public void OnBrushSelected(OnBrushSelectedArgs arg)
        {
            Color color = arg.BrushType == _brush.BrushType ? _selectedColor : _unselectedColor;
            _background.color = color;
        }
    }

    public class OnBrushSelectedArgs : EventArgs
    {
        public eBrushType BrushType;
        public OnBrushSelectedArgs(eBrushType brushType)
        {
            BrushType = brushType;
        }
    }
}
