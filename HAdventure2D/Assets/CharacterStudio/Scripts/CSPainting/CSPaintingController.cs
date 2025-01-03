using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPaintingController : MonoBehaviour
    {
        [SerializeField] RectTransform _paintingCanvas;
        private Vector3 _lastMousePosition;

        private void Update()
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
