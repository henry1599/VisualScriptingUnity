using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class PaletteManager : MonoBehaviour
    {
        [SerializeField] Transform _paletteContainer;
        [SerializeField] PaletteButton _paletteButtonPrefab;
        [SerializeField] List<Color> _defaultColors;

        List<PaletteButton> _paletteButtons;
        EventSubscription<OnRemoveColorArgs> _removeColorSubscription;
        private void Awake()
        {
            InitPalette();

            _removeColorSubscription = EventBus.Instance.Subscribe<OnRemoveColorArgs>( OnRemoveColor );
        }

        private void OnRemoveColor( OnRemoveColorArgs args )
        {
            _paletteButtons.Remove( args.Btn );
            Destroy( args.Btn.gameObject );
        }

        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _removeColorSubscription );
        }
        void InitPalette()
        {
            _paletteButtons = new List<PaletteButton>();
            int childCount = _paletteContainer.childCount;
            for (int i = childCount - 1; i >= 0; i-- )
            {
                Destroy( _paletteContainer.GetChild( i ).gameObject );
            }
            foreach ( var color in _defaultColors )
            {
                var button = Instantiate( _paletteButtonPrefab, _paletteContainer );
                button.Setup( color );
                _paletteButtons.Add( button );
            }
        }
    }
}
