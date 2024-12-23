using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterStudio
{
    public class ToolbarItem : MonoBehaviour
    {
        public int ID;
        public bool HasContextMenu;
        public string Text;
        [SerializeField] protected Transform _itemContainer;
        [ShowIf(nameof(HasContextMenu)), SerializeField] protected GameObject _item;
        [ShowIf(nameof(HasContextMenu))] public List<ToolbarContextMenuItem> ContextMenuList;
        [HideIf(nameof(HasContextMenu))] public UnityEvent OnClick;
        private bool _isShowing = false;
        public void ShowContextMenu()
        {
            if (!HasContextMenu)
            {
                return;
            }
            int childCount = _itemContainer.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(_itemContainer.GetChild(i).gameObject);
            }
            if (_isShowing)
            {
                _isShowing = false;
                return;
            }
            _isShowing = true;
            float width = 0;
            foreach (var item in ContextMenuList)
            {
                width = Mathf.Max(width, GetTextLength(item.Text));
                var contextMenuItem = Instantiate(_item, _itemContainer).GetComponent<ContextMenuItem>();
                contextMenuItem.Setup(item.Text, item.OnClick);
            }
            RectTransform rectTransform = _itemContainer.GetComponent<RectTransform>();
            var size = rectTransform.sizeDelta;
            size.x = width;
            rectTransform.sizeDelta = size;
        }
        float GetTextLength(string text)
        {
            return text.Length * 20;
        }
    }
}
