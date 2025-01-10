using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] RectTransform rectTransform;
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text descriptionText;
        TooltipData currentData = null;
        Vector2 offset = new Vector2(10, 10);
        public void Show(Canvas mainCanvas, TooltipData data, Vector2 mousePosition)
        {
            if (currentData != null && currentData.Equals(data))
            {
                return;
            }
            titleText.gameObject.SetActive(!string.IsNullOrEmpty(data.Title));
            descriptionText.gameObject.SetActive(!string.IsNullOrEmpty(data.Description));

            titleText.text = data.Title;
            descriptionText.text = data.Description;
            float widthPerCharacter = 20;
            float width = Mathf.Max(titleText.textInfo.characterCount, descriptionText.textInfo.characterCount) * widthPerCharacter;
            float height = Mathf.Max(titleText.preferredHeight + descriptionText.preferredHeight, 100);
            rectTransform.sizeDelta = new Vector2(width, height);

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            Vector2 adjustedPosition = mousePosition;
            float diffX = screenSize.x - (mousePosition.x + rectTransform.sizeDelta.x + offset.x);
            float diffY = screenSize.y - (mousePosition.y + rectTransform.sizeDelta.y + offset.y);
            if (diffX >= 0 && diffY >= 0)
            {
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 0);
                rectTransform.pivot = new Vector2(0, 0);
                adjustedPosition += new Vector2(offset.x, offset.y);
            }
            else if (diffX < 0 && diffY >= 0)
            {
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.pivot = new Vector2(1, 0);
                adjustedPosition += new Vector2(-offset.x, offset.y);
            }
            else if (diffX >= 0 && diffY < 0)
            {
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.pivot = new Vector2(0, 1);
                adjustedPosition += new Vector2(offset.x, -offset.y);
            }
            else if (diffX < 0 && diffY < 0)
            {
                rectTransform.anchorMin = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.pivot = new Vector2(1, 1);
                adjustedPosition += new Vector2(-offset.x, -offset.y);
            }

            rectTransform.position = adjustedPosition;
            rectTransform.SetParent(mainCanvas.transform);
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
    [Serializable]
    public class TooltipData
    {
        public string Title;
        public string Description;
    }
}
