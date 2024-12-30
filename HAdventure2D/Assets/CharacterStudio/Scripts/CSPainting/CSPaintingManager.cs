using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterStudio
{
    public class CSPaintingManager : MonoBehaviour
    {
        [SerializeField] private CSImage _csImage;
        [SerializeField] private CSPaintingRenderer _paintingRenderer;
        [SerializeField] private CSBrush _brush;

        EventSubscription<PointerDownArgs> _pointerDownSubscription;
        EventSubscription<PointerMoveArgs> _pointerMoveSubscription;
        EventSubscription<PointerUpArgs> _pointerUpSubscription;

        private Color cuurentColor = Color.yellow;

        private void Awake()
        {
            _pointerDownSubscription = EventBus.Instance.Subscribe<PointerDownArgs>( OnPointerDown );
            _pointerUpSubscription = EventBus.Instance.Subscribe<PointerUpArgs>( OnPointerUp );
            ReloadBrush();
        }
        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _pointerDownSubscription );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
            EventBus.Instance.Unsubscribe( _pointerUpSubscription );
        }
        private void OnPointerDown( PointerDownArgs args )
        {
            StartDrawing( args.Data );
        }
        private void OnPointerMove( PointerMoveArgs args )
        {
            Draw( args.Data );
        }
        private void OnPointerUp( PointerUpArgs args )
        {
            StopDrawing();
        }
        void ReloadBrush()
        {
            _brush.Initialize( _paintingRenderer );
        }

        void StopDrawing()
        {
            _brush.DrawPointerUp( Vector2.zero, cuurentColor );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
        }
        void Draw(PointerEventData evt)
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _brush.DrawPointerMove( normalizedVector, cuurentColor );
        }
        void StartDrawing( PointerEventData evt )
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _brush.DrawPointerDown( normalizedVector, cuurentColor );
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerMove );
        }
        [Button("Clear")]
        public void Clear()
        {
            _paintingRenderer.ClearCanvas();
        }
    }
}
