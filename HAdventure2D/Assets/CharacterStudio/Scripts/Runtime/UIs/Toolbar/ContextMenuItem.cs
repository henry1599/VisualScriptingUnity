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
        public void Setup(ToolbarContextMenuItem item)
        {
            _button.interactable = !item.EditorExclusive;
            _button.onClick.AddListener(() => item.OnClick?.Invoke());
            _text.text = item.Text;
            Tooltipable tooltipable = gameObject.GetComponent<Tooltipable>();
            if (tooltipable == null)
            {
                tooltipable = gameObject.AddComponent<Tooltipable>();
            }
            tooltipable.Data = item.Tooltip;
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
