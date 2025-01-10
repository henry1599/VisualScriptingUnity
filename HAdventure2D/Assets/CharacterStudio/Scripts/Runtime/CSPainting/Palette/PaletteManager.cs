using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PaletteManager : MonoBehaviour
    {
        [SerializeField] Transform _paletteContainer;
        [SerializeField] PaletteButton _paletteButtonPrefab;
        [SerializeField] Button _addButton;
        [SerializeField] Button _resetToDefaultButton;
        [SerializeField] Button _clearPaletteButton;

        List<PaletteButton> _paletteButtons;
        EventSubscription<OnRemoveColorArgs> _removeColorSubscription;
        private void Awake()
        {
            InitPalette();

            _addButton.onClick.AddListener( OnAddButtonClick );
            _resetToDefaultButton.onClick.AddListener( OnResetToDefaultButtonClick );
            _clearPaletteButton.onClick.AddListener( OnClearPaletteButtonClick );
            _removeColorSubscription = EventBus.Instance.Subscribe<OnRemoveColorArgs>( OnRemoveColor );
        }

        private void OnClearPaletteButtonClick()
        {
            _paletteButtons = new List<PaletteButton>();
            int childCount = _paletteContainer.childCount;
            for (int i = childCount - 1; i >= 0; i-- )
            {
                Destroy( _paletteContainer.GetChild( i ).gameObject );
            }
        }

        private void OnResetToDefaultButtonClick()
        {
            InitPalette();
        }

        private void OnAddButtonClick()
        {
            Color currentColor = CSPaintingManager.Instance.CuurentColor;
            if ( _paletteButtons.Exists( x => x.Color == currentColor ) )
            {
                return;
            }
            var button = Instantiate( _paletteButtonPrefab, _paletteContainer );
            button.Setup( currentColor );
            _paletteButtons.Add( button );
        }

        private void OnRemoveColor( OnRemoveColorArgs args )
        {
            _paletteButtons.Remove( args.Btn );
            Destroy( args.Btn.gameObject );
        }

        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _removeColorSubscription );
            _addButton.onClick.RemoveAllListeners();
            _resetToDefaultButton.onClick.RemoveAllListeners();
            _clearPaletteButton.onClick.RemoveAllListeners();
        }
        void InitPalette()
        {
            _paletteButtons = new List<PaletteButton>();
            int childCount = _paletteContainer.childCount;
            for (int i = childCount - 1; i >= 0; i-- )
            {
                Destroy( _paletteContainer.GetChild( i ).gameObject );
            }
            foreach ( var color in CSPaintingManager.Instance.Setting.DefaultPalette )
            {
                var button = Instantiate( _paletteButtonPrefab, _paletteContainer );
                button.Setup( color );
                _paletteButtons.Add( button );
            }
        }
    }
}
