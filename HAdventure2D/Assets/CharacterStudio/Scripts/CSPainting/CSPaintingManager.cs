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
        [SerializeField] private CSPaintingRenderer _paintingPreview;
        [SerializeField] private CSPaintingRenderer _paintingHover;
        [SerializeField] private CSBrush _brush;

        EventSubscription<PointerDownArgs> _pointerDownSubscription;
        EventSubscription<PointerMoveArgs> _pointerMoveSubscription;
        EventSubscription<PointerMoveArgs> _pointerHoverSubscription;
        EventSubscription<PointerUpArgs> _pointerUpSubscription;

        private Color cuurentColor = Color.yellow;

        private void Awake()
        {
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerHover );
            _pointerDownSubscription = EventBus.Instance.Subscribe<PointerDownArgs>( OnPointerDown );
            _pointerUpSubscription = EventBus.Instance.Subscribe<PointerUpArgs>( OnPointerUp );
            ReloadBrush();
        }
        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _pointerDownSubscription );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
            EventBus.Instance.Unsubscribe( _pointerUpSubscription );
            EventBus.Instance.Unsubscribe( _pointerHoverSubscription );
        }

        private void OnPointerHover(PointerMoveArgs args)
        {
            DrawHover(args.Data);
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
            _brush.Initialize( _paintingRenderer, _paintingPreview, _paintingHover );
        }

        void StopDrawing()
        {
            _brush.DrawPointerUp( eCanvasType.Main, Vector2.zero, cuurentColor );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
        }
        void Draw(PointerEventData evt)
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _brush.DrawPointerMove( eCanvasType.Main, normalizedVector, cuurentColor );
        }
        void DrawHover( PointerEventData evt )
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _brush.DrawOnHover( normalizedVector, cuurentColor );
        }
        void StartDrawing( PointerEventData evt )
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _brush.DrawPointerDown( eCanvasType.Main, normalizedVector, cuurentColor );
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerMove );
        }
        [Button("Clear")]
        public void Clear()
        {
            _paintingRenderer.ClearCanvas();
        }
    }
}
