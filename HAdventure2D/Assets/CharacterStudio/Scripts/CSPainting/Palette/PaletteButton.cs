using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PaletteButton : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] Button _button;
        [SerializeField] Button _removeButton;
        Color _color;
        public void Setup( Color color )
        {
            this._color = color;
            _icon.color = color;
            _button.onClick.AddListener( OnClick );
            _removeButton.onClick.AddListener( OnRemoveButtonClick );
            SetRemoveButton( false );
        }
        public void SetRemoveButton( bool value )
        {
            _removeButton.gameObject.SetActive( value );
        }
        void OnClick()
        {
            EventBus.Instance.Publish( new OnColorPickedArgs( _color ) );
        }
        void OnRemoveButtonClick()
        {
            EventBus.Instance.Publish( new OnRemoveColorArgs( this ) );
        }
    }
    public class OnRemoveColorArgs : EventArgs
    {
        public PaletteButton Btn { get; private set; }
        public OnRemoveColorArgs( PaletteButton btn )
        {
            this.Btn = btn;
        }
    }
}
