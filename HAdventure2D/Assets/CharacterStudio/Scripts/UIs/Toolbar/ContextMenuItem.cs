using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class ContextMenuItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        public void Setup(string text, UnityEvent action)
        {
            _button.onClick.AddListener(() => action?.Invoke());
            _text.text = text;
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
