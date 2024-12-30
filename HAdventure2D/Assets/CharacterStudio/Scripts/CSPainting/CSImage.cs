using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterStudio
{
    public class CSImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        protected RectTransform rectTransform
        {
            get
            {
                if ( _rectTransform == null )
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }
        private RectTransform _rectTransform;
        private float width => rectTransform?.rect.width ?? 1080;
        private float height => rectTransform?.rect.height ?? 1080;
        public void OnPointerDown( PointerEventData eventData )
        {
            Vector2 localPosition;
            if ( RectTransformUtility.ScreenPointToLocalPointInRectangle( rectTransform, eventData.position, eventData.pressEventCamera, out localPosition ) )
            {
                Vector2 normalizedPosition = ProcessPosition( localPosition );
                EventBus.Instance?.Publish( new PointerDownArgs( this, eventData, normalizedPosition ) );
            }
        }
        public void OnPointerMove( PointerEventData eventData )
        {
            Vector2 localPosition;
            if ( RectTransformUtility.ScreenPointToLocalPointInRectangle( rectTransform, eventData.position, eventData.pressEventCamera, out localPosition ) )
            {
                Vector2 normalizedPosition = ProcessPosition( localPosition );
                EventBus.Instance?.Publish( new PointerMoveArgs( this, eventData, normalizedPosition ) );
            }
        }
        public void OnPointerUp( PointerEventData eventData )
        {
            EventBus.Instance?.Publish( new PointerUpArgs( this, eventData ) );
        }
        private Vector2 ProcessPosition( Vector2 localMousePosition )
        {
            Vector2 normalizedPosition = NormalizePixelPosition( localMousePosition );
            return normalizedPosition;
        }

        private Vector2 NormalizePixelPosition( Vector2 pixelPosition )
        {
            float normalizedX = Mathf.InverseLerp( 0, width, pixelPosition.x + width / 2f );
            float normalizedY = Mathf.InverseLerp( 0, height, pixelPosition.y + height / 2f );
            return new( normalizedX, normalizedY );
        }
    }
    public class PointerArgs : EventArgs
    {
        public CSImage Image;
        public PointerEventData Data;
        public PointerArgs( CSImage image, PointerEventData evt )
        {
            Image = image;
            Data = evt;
        }
    }
    public class PointerDownArgs : PointerArgs
    {
        public Vector2 NormalizedPosition;
        public PointerDownArgs( CSImage image, PointerEventData evt, Vector2 normalizedPosition ) : base( image, evt )
        {
            NormalizedPosition = normalizedPosition;
        }
    }
    public class PointerMoveArgs : PointerArgs
    {
        public Vector2 NormalizedPosition;
        public PointerMoveArgs( CSImage image, PointerEventData evt, Vector2 normalizedPosition ) : base( image, evt ) 
        {
            NormalizedPosition = normalizedPosition;
        }
    }
    public class PointerUpArgs : PointerArgs
    {
        public PointerUpArgs( CSImage image, PointerEventData evt ) : base( image, evt ) { }
    }
}
