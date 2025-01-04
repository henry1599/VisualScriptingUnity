using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterStudio
{
    public enum eDirection
    {
        Horizontal,
        Vertical
    }
    public enum eScaleDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class ScaleHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] RectTransform mainRect;
        [Tooltip("Indicate the scale direction of the panel")]
        [SerializeField] eDirection direction;
        [Tooltip("Indicate which direction makes the panel expand")]
        [SerializeField] eScaleDirection expandDirection;
        [MinMaxSlider(50, 1920), SerializeField] Vector2 sizeRange;
        [SerializeField] Texture2D scaleCursor;

        private bool isDragging = false;

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;

            Vector2 delta = eventData.delta;

            if (direction == eDirection.Horizontal)
            {
                int scaleFactor = expandDirection == eScaleDirection.Right ? 1 : -1;
                float newWidth = Mathf.Clamp(mainRect.sizeDelta.x + (scaleFactor * delta.x), sizeRange.x, sizeRange.y);
                mainRect.sizeDelta = new Vector2(newWidth, mainRect.sizeDelta.y);
            }
            else if (direction == eDirection.Vertical)
            {
                int scaleFactor = expandDirection == eScaleDirection.Up ? 1 : -1;
                float newHeight = Mathf.Clamp(mainRect.sizeDelta.y + (scaleFactor * delta.y), sizeRange.x, sizeRange.y);
                mainRect.sizeDelta = new Vector2(mainRect.sizeDelta.x, newHeight);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Optionally, change the cursor or visual feedback
            Cursor.SetCursor(scaleCursor, new Vector2(scaleCursor.width / 2f, scaleCursor.height / 2f), CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isDragging) return;
            // Optionally, revert the cursor or visual feedback
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}