using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSPaintingManager : MonoSingleton<CSPaintingManager>
    {
        [Header("Setting")]
        [SerializeField] private CSPaintingSetting _paintingSetting;
        [SerializeField] private CanvasGroup _canvasGroup;
        [Space(5)]




        [Header("Reference")]
        [Label("Main Painter"), SerializeField] private CSImage _csImage;
        [Foldout("Renderers"), SerializeField] private CSPaintingRenderer _paintingRenderer;
        [Foldout("Renderers"), SerializeField] private CSPaintingRenderer _paintingPreview;
        [Foldout("Renderers"), SerializeField] private CSPaintingRenderer _paintingHover;
        [Foldout("Renderers"), SerializeField] private CSPaintingBackgroundRenderer _backgroundRenderer;
        [Foldout("Renderers"), SerializeField] private Image _guideLineRenderer;
        [SerializeField] private Transform _brushContainer;
        [SerializeField] private Transform _brushUIContainer;
        [SerializeField] private FlexibleColorPicker _colorPicker;
        [Space(5)]



        [Header("Brush")]
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
        EventSubscription<OnUndoArg> _undoSubscription;
        EventSubscription<OnRedoArg> _redoSubscription;
        EventSubscription<SavePaintingArg> _savePaintingSubscription;

        public CSPaintingSetting Setting => _paintingSetting;
        public Color CuurentColor => _colorPicker.color;
        public CSBrush ActiveBrush => _activeBrush;
        public bool IsSetup {get; private set;} = false;
        public eCharacterPart ChosenPart {get; private set;}

        protected override bool Awake()
        {
            return base.Awake();
        }
        public void Setup()
        {
            var part = CharacterStudioMain.Instance.SelectedCategory;   
            ChosenPart = part;
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerHover );
            _pointerDownSubscription = EventBus.Instance.Subscribe<PointerDownArgs>( OnPointerDown );
            _pointerUpSubscription = EventBus.Instance.Subscribe<PointerUpArgs>( OnPointerUp );
            _pointerEnterSubscription = EventBus.Instance.Subscribe<PointerEnterArgs>( OnPointerEnter );
            _pointerExitSubscription = EventBus.Instance.Subscribe<PointerExitArgs>( OnPointerExit );
            _brushSelectedSubscription = EventBus.Instance.Subscribe<OnBrushSelectedArgs>( OnBrushSelected );
            _colorPickedSubscription = EventBus.Instance.Subscribe<OnColorPickedArgs>( OnColorPicked );
            _undoSubscription = EventBus.Instance.Subscribe<OnUndoArg>( OnUndo );
            _redoSubscription = EventBus.Instance.Subscribe<OnRedoArg>( OnRedo );
            _savePaintingSubscription = EventBus.Instance.Subscribe<SavePaintingArg>( OnSavePainting );

            _backgroundRenderer.Setup( _paintingSetting );
            Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
            Texture2D texture = DataManager.Instance.CharacterDatabase.GetInstructionTexture(part);
            if (texture == null)
            {
                Debug.LogError("Texture not found");
                return;
            }
            _guideLineRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);


            
            foreach (var brushPrefab in _paintingSetting.GetAllBrushes())
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

            IsSetup = true;
        }

        private void OnSavePainting(SavePaintingArg arg)
        {
            Texture2D paintingTexture = GetPaintingTexture();
            string folderPath = Path.Combine(
                DataManager.Instance.SaveData.DataFolderPath,
                ChosenPart.ToString()
            );
            int fileCount = CSUtils.GetFileCount(folderPath, "*.csi");
            string path = Path.Combine(
                DataManager.Instance.SaveData.DataFolderPath,
                ChosenPart.ToString(),
                $"{ChosenPart.ToString()}{fileCount + 1}.csi"
            );
            CSIFile.SaveAsCsiFile(paintingTexture, path);
            Debug.Log($"Painting saved to: {path}");
            DataManager.Instance.InitConfigs();
        }

        public string GetPartDisplayName()
        {
            return DataManager.Instance.CharacterDatabase.GetCategoryDisplayName(ChosenPart);
        }
        public Texture2D GetPaintingTexture()
        {
            return _paintingRenderer.DrawingTexture;
        }
        public void Unsetup()
        {
            EventBus.Instance.Unsubscribe( _pointerDownSubscription );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
            EventBus.Instance.Unsubscribe( _pointerUpSubscription );
            EventBus.Instance.Unsubscribe( _pointerHoverSubscription );
            EventBus.Instance.Unsubscribe( _pointerEnterSubscription );
            EventBus.Instance.Unsubscribe( _pointerExitSubscription );
            EventBus.Instance.Unsubscribe( _brushSelectedSubscription );
            EventBus.Instance.Unsubscribe( _colorPickedSubscription );
            EventBus.Instance.Unsubscribe( _undoSubscription );
            EventBus.Instance.Unsubscribe( _redoSubscription );
            EventBus.Instance.Unsubscribe( _savePaintingSubscription );
            _activeBrush?.Unsetup();
            foreach (var brush in _brushes.Values)
            {
                brush.Unsetup();
            }
            int brushCount = _brushContainer.childCount;
            for (int i = brushCount - 1; i >= 0; i--)
            {
                Destroy(_brushContainer.GetChild(i).gameObject);
            }
            brushCount = _brushUIContainer.childCount;
            for (int i = brushCount - 1; i >= 0; i--)
            {
                Destroy(_brushUIContainer.GetChild(i).gameObject);
            }
            _brushes.Clear();
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;

            _paintingHover.ClearCanvas();
            _paintingPreview.ClearCanvas();
            _paintingRenderer.ClearCanvas();
            _backgroundRenderer.ClearCanvas();
            IsSetup = false;
        }
        private void Update()
        {
            if ( !IsSetup )
                return;
            if ( Input.GetKey( KeyCode.LeftControl ) || Input.GetKey( KeyCode.RightControl ) )
            {
                float mouseVScroll = Input.mouseScrollDelta.y;
                if ( mouseVScroll < 0 )
                {
                    _activeBrush?.DecreaseSize();
                }
                else if ( mouseVScroll > 0 )
                {
                    _activeBrush?.IncreaseSize();
                }
            }
        }

        private void OnUndo(OnUndoArg arg)
        {
            CSState state = CSStateManager.Instance.GetUndoState();
            if (state == null)
                return;
            CSPaintingRenderer.LoadFromState(_paintingRenderer, state);
        }

        private void OnRedo(OnRedoArg arg)
        {
            CSState state = CSStateManager.Instance.GetRedoState();
            if (state == null)
                return;
            CSPaintingRenderer.LoadFromState(_paintingRenderer, state);
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
