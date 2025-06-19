using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class UISaveItem : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] TMP_Text _name;
        [SerializeField] Button _thisButton;
        [SerializeField] Button _removeButton;
        [SerializeField] Color _selectedColor;
        [SerializeField] Color _defaultColor;
        [SerializeField] Image _background;

        Action<string> _removeCallback;
        public static Action<UISaveItem> OnItemSelected;
        public ProgressSaveData SaveData { get; private set; }
        private void Start()
        {
            OnItemSelected += HandleSelection;
        }
        private void OnDestroy()
        {
            OnItemSelected -= HandleSelection;
        }

        private void HandleSelection(UISaveItem item)
        {
            Color color = item == this ? _selectedColor : _defaultColor;
            if (_background != null)
            {
                _background.color = color;
            }
        }

        public void Setup(Sprite icon, string name, ProgressSaveData saveData, Action<string> removeCallback)
        {
            SaveData = saveData;
            if (_icon != null)
            {
                _icon.sprite = icon;
            }
            if (_name != null)
            {
                _name.text = name;
            }
            _removeCallback = removeCallback;
            _removeButton.onClick.AddListener(OnRemoveButtonClick);
            _thisButton.onClick.AddListener(OnItemClick);
        }

        private void OnItemClick()
        {
            OnItemSelected?.Invoke(this);
        }

        private void OnRemoveButtonClick()
        {
            Destroy(gameObject);
            _removeCallback?.Invoke(_name.text);
        }
    }
}
