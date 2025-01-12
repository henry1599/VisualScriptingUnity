using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class UIManager : MonoBehaviour
    {
        public static bool IsMouseOverUIGameObject(GameObject uiGameObject)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject == uiGameObject)
                {
                    return true;
                }
            }

            return false;
        }
        public static GameObject GetCurrentUIObjectMouseIsOver()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                return raycastResults[0].gameObject;
            }

            return null;
        }
        public static Tooltipable GetFirstTooltipableObjectOnMouseHover()
        {
            GameObject uiObjectMouseIsOver = GetCurrentUIObjectMouseIsOver();

            if (uiObjectMouseIsOver != null)
            {
                Tooltipable tooltipable = uiObjectMouseIsOver.GetComponent<Tooltipable>();

                if (tooltipable != null)
                {
                    return tooltipable;
                }

                tooltipable = uiObjectMouseIsOver.GetComponentInParent<Tooltipable>();
                if (tooltipable != null)
                {
                    return tooltipable;
                }
            }

            return null;
        }
    }
}
