using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterStudio
{
    public class CSImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler, IPointerExitHandler, IPointerEnterHandler
    {
        public RectTransform RectTransform
        {
            get
            {
                if ( _rectTransform == null )
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }
        private RectTransform _rectTransform;
        public void OnPointerDown( PointerEventData eventData )
        {
            if ( eventData.button != PointerEventData.InputButton.Left )
            {
                EventBus.Instance?.Publish( new PointerUpArgs( this, eventData ) );
                return;
            }
            EventBus.Instance?.Publish( new PointerDownArgs( this, eventData ) );
        }
        public void OnPointerMove( PointerEventData eventData )
        {
            if ( eventData.button != PointerEventData.InputButton.Left )
                return;
            EventBus.Instance?.Publish( new PointerMoveArgs( this, eventData ) );
        }
        public void OnPointerUp( PointerEventData eventData )
        {
            if ( eventData.button != PointerEventData.InputButton.Left )
                return;
            EventBus.Instance?.Publish( new PointerUpArgs( this, eventData ) );
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventBus.Instance?.Publish( new PointerExitArgs( this, eventData ) );
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventBus.Instance?.Publish( new PointerEnterArgs( this, eventData ) );
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
        public PointerDownArgs( CSImage image, PointerEventData evt ) : base( image, evt ) { }
    }
    public class PointerMoveArgs : PointerArgs
    {
        public PointerMoveArgs( CSImage image, PointerEventData evt ) : base( image, evt ) { }
    }
    public class PointerUpArgs : PointerArgs
    {
        public PointerUpArgs( CSImage image, PointerEventData evt ) : base( image, evt ) { }
    }
    public class PointerExitArgs : PointerArgs
    {
        public PointerExitArgs( CSImage image, PointerEventData evt ) : base( image, evt ) { }
    }
    public class PointerEnterArgs : PointerArgs
    {
        public PointerEnterArgs( CSImage image, PointerEventData evt ) : base( image, evt ) { }
    }
}
