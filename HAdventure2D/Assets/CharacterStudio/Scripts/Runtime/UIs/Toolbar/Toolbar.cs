using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class Toolbar : MonoBehaviour
    {
        public List<ToolbarItem> _toolbarItemPrefabs;
        [SerializeField] Transform _itemContainer;
        [SerializeField] GameObject _separattor;
        List<ToolbarItem> _toolbarItems;
        private void Awake()
        {
            _toolbarItems = new List<ToolbarItem>();
            int childCount = _itemContainer.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(_itemContainer.GetChild(i).gameObject);
            }
            foreach (var item in _toolbarItemPrefabs)
            {
                var toolbarItem = Instantiate(item, _itemContainer).GetComponent<ToolbarItem>();
                _toolbarItems.Add(toolbarItem);
                Instantiate(_separattor, _itemContainer);
            }
        }
    }
}
