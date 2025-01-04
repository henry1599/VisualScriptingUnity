using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPaintingController : MonoBehaviour
    {
        [SerializeField] RectTransform _paintingCanvas;
        private Vector3 _lastMousePosition;
        private CSBrush _cacheBrush = null;

        private void Update()
        {
            HandleMouseControl();
            HandleQuickEyeDrop();
            HandleUndoRedohortcut();
        }
        void HandleUndoRedohortcut()
        {
            // if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            // {
            //     if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            //     {
            //         if (Input.GetKeyDown(KeyCode.Z))
            //         {
            //             EventBus.Instance.Publish(new OnRedoArg());
            //         }
            //     }
            //     else
            //     {
            //         if (Input.GetKeyDown(KeyCode.Z))
            //         {
            //             EventBus.Instance.Publish(new OnUndoArg());
            //         }
            //         if (Input.GetKeyDown(KeyCode.Y))
            //         {
            //             EventBus.Instance.Publish(new OnRedoArg());
            //         }
            //     }
            // }
            if (Input.GetKeyDown(KeyCode.A))
            {
                EventBus.Instance.Publish(new OnUndoArg());
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                EventBus.Instance.Publish(new OnRedoArg());
            }
        }
        void HandleQuickEyeDrop()
        {
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (_cacheBrush == null)
                    _cacheBrush = CSPaintingManager.Instance.ActiveBrush;
                EventBus.Instance.Publish(new OnBrushSelectedArgs(eBrushType.EyeDropper));
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
            {
                EventBus.Instance.Publish(new OnBrushSelectedArgs(_cacheBrush.BrushType));
                _cacheBrush = null;
            }
        }
        void HandleMouseControl()
        {
            if ( Input.GetMouseButton( 2 ) )
            {
                HandleMoveView();
            }
            float mouseScrollY = Input.mouseScrollDelta.y;
            if ( mouseScrollY < 0 )
            {
                HandleZoom( false );
            }
            else if ( mouseScrollY > 0 )
            {
                HandleZoom( true );
            }
        }
        void HandleMoveView()
        {
            if ( Input.GetMouseButtonDown( 2 ) )
            {
                _lastMousePosition = Input.mousePosition;
            }

            if ( Input.GetMouseButton( 2 ) )
            {
                Vector3 delta = Input.mousePosition - _lastMousePosition;
                _paintingCanvas.anchoredPosition += new Vector2( delta.x, delta.y );
                _lastMousePosition = Input.mousePosition;
            }
        }

        void HandleZoom( bool isZoomIn )
        {
            float zoomFactor = isZoomIn ? 1.1f : 0.9f;
            Vector2 mousePosition = Input.mousePosition;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle( _paintingCanvas, mousePosition, null, out localPoint );

            Vector2 pivotOffset = _paintingCanvas.pivot - new Vector2( 0.5f, 0.5f );
            Vector2 newSize = _paintingCanvas.sizeDelta * zoomFactor;
            Vector2 sizeDeltaChange = newSize - _paintingCanvas.sizeDelta;

            Vector2 newAnchoredPosition = _paintingCanvas.anchoredPosition - new Vector2( localPoint.x * sizeDeltaChange.x / _paintingCanvas.rect.width, localPoint.y * sizeDeltaChange.y / _paintingCanvas.rect.height );

            _paintingCanvas.sizeDelta = newSize;
            _paintingCanvas.anchoredPosition = newAnchoredPosition;
        }
    }
}
