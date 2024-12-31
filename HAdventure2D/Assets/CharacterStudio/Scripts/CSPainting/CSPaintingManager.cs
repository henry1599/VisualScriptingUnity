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
        [Header("Setting")]
        [SerializeField] private CSPaintingSetting _paintingSetting;
        [Space(5)]



        [Header("Reference")]
        [Label("Main Painter"), SerializeField] private CSImage _csImage;
        [Foldout("Renderers"), SerializeField] private CSPaintingRenderer _paintingRenderer;
        [Foldout("Renderers"), SerializeField] private CSPaintingRenderer _paintingPreview;
        [Foldout("Renderers"), SerializeField] private CSPaintingRenderer _paintingHover;
        [Foldout("Renderers"), SerializeField] private CSPaintingBackgroundRenderer _backgroundRenderer;
        [Space(5)]



        [Header("Brush")]
        [SerializeField] private CSBrush _brush;

        EventSubscription<PointerDownArgs> _pointerDownSubscription;
        EventSubscription<PointerMoveArgs> _pointerMoveSubscription;
        EventSubscription<PointerMoveArgs> _pointerHoverSubscription;
        EventSubscription<PointerUpArgs> _pointerUpSubscription;
        EventSubscription<PointerEnterArgs> _pointerEnterSubscription;
        EventSubscription<PointerExitArgs> _pointerExitSubscription;

        private Color cuurentColor = Color.yellow;

        private void Awake()
        {
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerHover );
            _pointerDownSubscription = EventBus.Instance.Subscribe<PointerDownArgs>( OnPointerDown );
            _pointerUpSubscription = EventBus.Instance.Subscribe<PointerUpArgs>( OnPointerUp );
            _pointerEnterSubscription = EventBus.Instance.Subscribe<PointerEnterArgs>( OnPointerEnter );
            _pointerExitSubscription = EventBus.Instance.Subscribe<PointerExitArgs>( OnPointerExit );

            ReloadBrush();
            _backgroundRenderer.Setup( _paintingSetting );
        }


        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _pointerDownSubscription );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
            EventBus.Instance.Unsubscribe( _pointerUpSubscription );
            EventBus.Instance.Unsubscribe( _pointerHoverSubscription );
            EventBus.Instance.Unsubscribe( _pointerEnterSubscription );
            EventBus.Instance.Unsubscribe( _pointerExitSubscription );
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
        private void OnPointerEnter(PointerEnterArgs args)
        {
            HandleCursor(true);
        }

        private void OnPointerExit(PointerExitArgs args)
        {
            HandleCursor(false);
        }






        void ReloadBrush()
        {
            _brush.Initialize( _paintingRenderer, _paintingPreview, _paintingHover );
        }
        void HandleCursor(bool isEnter)
        {
            _brush.HandleCursor( isEnter );
            _paintingHover.ClearCanvas();
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
