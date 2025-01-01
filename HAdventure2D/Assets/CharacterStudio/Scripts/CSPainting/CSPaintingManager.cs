using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterStudio
{
    public class CSPaintingManager : MonoSingleton<CSPaintingManager>
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
        [SerializeField] private Transform _brushContainer;
        [SerializeField] private Transform _brushUIContainer;
        [SerializeField] private FlexibleColorPicker _colorPicker;
        [Space(5)]



        [Header("Brush")]
        [SerializeField] private List<CSBrush> _brushPrefabs;
        [SerializeField] private CSBrushUI _brushUIPrefab;


        Dictionary<eBrushType, CSBrush> _brushes = new ();
        Dictionary<eBrushType, CSBrushUI> _brushUIs = new ();
        CSBrush _activeBrush;
        EventSubscription<PointerDownArgs> _pointerDownSubscription;
        EventSubscription<PointerMoveArgs> _pointerMoveSubscription;
        EventSubscription<PointerMoveArgs> _pointerHoverSubscription;
        EventSubscription<PointerUpArgs> _pointerUpSubscription;
        EventSubscription<PointerEnterArgs> _pointerEnterSubscription;
        EventSubscription<PointerExitArgs> _pointerExitSubscription;
        EventSubscription<OnBrushSelectedArgs> _brushSelectedSubscription;
        EventSubscription<OnColorPickedArgs> _colorPickedSubscription;



        public CSPaintingSetting Setting => _paintingSetting;
        public Color CuurentColor => _colorPicker.color;

        protected override bool Awake()
        {
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerHover );
            _pointerDownSubscription = EventBus.Instance.Subscribe<PointerDownArgs>( OnPointerDown );
            _pointerUpSubscription = EventBus.Instance.Subscribe<PointerUpArgs>( OnPointerUp );
            _pointerEnterSubscription = EventBus.Instance.Subscribe<PointerEnterArgs>( OnPointerEnter );
            _pointerExitSubscription = EventBus.Instance.Subscribe<PointerExitArgs>( OnPointerExit );
            _brushSelectedSubscription = EventBus.Instance.Subscribe<OnBrushSelectedArgs>( OnBrushSelected );
            _colorPickedSubscription = EventBus.Instance.Subscribe<OnColorPickedArgs>(OnColorPicked);

            _backgroundRenderer.Setup( _paintingSetting );

            return base.Awake();
        }


        void Start()
        {
            foreach (var brushPrefab in _brushPrefabs)
            {
                CSBrush brush = Instantiate( brushPrefab, _brushContainer );
                brush.Setup( _paintingRenderer, _paintingPreview, _paintingHover );
                brush.gameObject.SetActive( false );
                _brushes.TryAdd( brush.BrushType, brush );
                
                CSBrushUI brushUI = Instantiate( _brushUIPrefab, _brushUIContainer );
                brushUI.Setup( brush );
                _brushUIs.TryAdd( brush.BrushType, brushUI );
            }
            SelectBrush(_paintingSetting.DefaultBrush);
            SetCurrentColor(_colorPicker.StartingColor);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventBus.Instance.Unsubscribe( _pointerDownSubscription );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
            EventBus.Instance.Unsubscribe( _pointerUpSubscription );
            EventBus.Instance.Unsubscribe( _pointerHoverSubscription );
            EventBus.Instance.Unsubscribe( _pointerEnterSubscription );
            EventBus.Instance.Unsubscribe( _pointerExitSubscription );
            EventBus.Instance.Unsubscribe( _brushSelectedSubscription );
            EventBus.Instance.Unsubscribe( _colorPickedSubscription );
        }
        private void OnColorPicked(OnColorPickedArgs args)
        {
            SetCurrentColor(args.CapturedColor);
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
        private void OnBrushSelected(OnBrushSelectedArgs args)
        {
            SelectBrush(args.BrushType);
        }






        public void SetCurrentColor(Color color)
        {
            _colorPicker.SetColor(color);
        }
        void SelectBrush(eBrushType brushType)
        {
            if (_activeBrush != null && _activeBrush.BrushType != brushType)
            {
                _activeBrush.Unsetup();
            }
            // * Reset all brushes
            _activeBrush = null;
            foreach (var brush in _brushes.Values)
            {
                brush.gameObject.SetActive( false );
            }

            // * Select brush
            if (!_brushes.TryGetValue(brushType, out _activeBrush))
            {
                Debug.LogError("Brush not found");
                return;
            }
            _activeBrush.gameObject.SetActive( true );

            if (!_brushUIs.TryGetValue(brushType, out CSBrushUI brushUI))
            {
                Debug.LogError("BrushUI not found");
                return;
            }
            brushUI.OnBrushSelected(new OnBrushSelectedArgs(brushType));
            ReloadBrush();
        }
        void ReloadBrush()
        {
            _activeBrush?.Setup( _paintingRenderer, _paintingPreview, _paintingHover );
        }
        void HandleCursor(bool isEnter)
        {
            _activeBrush.HandleCursor( isEnter );
            _paintingHover.ClearCanvas();
        }
        void StopDrawing()
        {
            _activeBrush.DrawPointerUp( eCanvasType.Main, Vector2.zero, CuurentColor );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
        }
        void Draw(PointerEventData evt)
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _activeBrush.DrawPointerMove( eCanvasType.Main, normalizedVector, CuurentColor );
        }
        void DrawHover( PointerEventData evt )
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _activeBrush.DrawOnHover( normalizedVector, CuurentColor );
        }
        void StartDrawing( PointerEventData evt )
        {
            Vector2 normalizedVector = CSUtils.GetNormalizedPositionOnPaintingCanvas( evt, _csImage.RectTransform );
            _activeBrush.DrawPointerDown( eCanvasType.Main, normalizedVector, CuurentColor );
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerMove );
        }
        [Button("Clear")]
        public void Clear()
        {
            _paintingRenderer.ClearCanvas();
        }
    }
}
