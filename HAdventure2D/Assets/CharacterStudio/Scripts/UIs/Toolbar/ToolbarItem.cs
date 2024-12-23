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

        public void ShowContextMenu()
        {
            if (!HasContextMenu)
            {
                return;
            }
            foreach (var item in ContextMenuList)
            {
                var contextMenuItem = Instantiate(_item, _itemContainer).GetComponent<ContextMenuItem>();
                contextMenuItem.Setup(item.Text, item.OnClick);
            }
        }
    }
}
