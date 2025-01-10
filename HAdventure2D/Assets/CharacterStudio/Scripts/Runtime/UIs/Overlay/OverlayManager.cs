using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class OverlayManager : MonoSingleton<OverlayManager>
    {
        [SerializeField] GameObject _overlayField;
        [SerializeField] Camera _characterCamera;



        [Header( "TOP" )]
        [BoxGroup( "Buttons" ), SerializeField] Toggle _showButton;
        [BoxGroup( "Buttons" ), SerializeField] Image _showButtonImage;


        [Header( "LEFT" )]
        [BoxGroup( " Buttons " ), SerializeField] Toggle _toggleToolButton;
        [BoxGroup( " Buttons " ), SerializeField] Image _toggleToolButtonImage;
        [BoxGroup( " Buttons " ), SerializeField] Button _resetButton;
        [BoxGroup( " Buttons " ), SerializeField] Button _hFlipButton;
        [BoxGroup( " Buttons " ), SerializeField] Button _vFlipButton;
        [BoxGroup( " Buttons " ), SerializeField] Button _randomButton;
        [BoxGroup( " Buttons " ), SerializeField] Toggle _toggleBackgroundButton;
        [BoxGroup( " Buttons " ), SerializeField] Image _toggleBackgroundButtonImage;
        [BoxGroup( " GameObject " ), SerializeField] GameObject _toolButtonField;
        [BoxGroup( " GameObject " ), SerializeField] GameObject _backgroundField;
        [BoxGroup( " GameObject " ), SerializeField] GameObject _scrollBackgroundField;
        [BoxGroup( " Transform " ), SerializeField] Transform _characterRenderer;

        [Header( "RIGHT" )]
        [BoxGroup( " Buttons " ), SerializeField] Button _zoomInButton;
        [BoxGroup( " Buttons " ), SerializeField] Button _zoomOutButton;


        [Header("SETTING")]
        [BoxGroup( "Setting" ), SerializeField] float _zoomSpeed = 0.1f;
        [BoxGroup( "Setting" ), SerializeField] float _defaultZoom = 0.32f;
        [BoxGroup( "Setting" ), SerializeField] float _minZoom = 0.12f;
        [BoxGroup( "Setting" ), SerializeField] float _maxZoom = 0.62f;
        [BoxGroup( "Setting" ), SerializeField] Sprite openEyeIcon;
        [BoxGroup( "Setting" ), SerializeField] Sprite closeEyeIcon;




        private float currentZoom;
        private bool showStatus = true;
        private bool toolStatus = false;
        private bool backgroundStatus = true;

        protected override bool Awake()
        {
            _showButton.onValueChanged.AddListener( OnShowButtonClicked );
            _toggleToolButton.onValueChanged.AddListener( OnToggleToolButtonClicked );
            _resetButton.onClick.AddListener( OnResetButtonClicked );
            _hFlipButton.onClick.AddListener( OnHFlipButtonClicked );
            _vFlipButton.onClick.AddListener( OnVFlipButtonClicked );
            _randomButton.onClick.AddListener( OnRandomButtonClicked );
            _toggleBackgroundButton.onValueChanged.AddListener( OnToggleBackgroundButtonClicked );
            _zoomInButton.onClick.AddListener( OnZoomInButtonClicked );
            _zoomOutButton.onClick.AddListener( OnZoomOutButtonClicked );


            _showButton.isOn = showStatus;
            _toggleToolButton.isOn = toolStatus;
            _toggleBackgroundButton.isOn = backgroundStatus;


            currentZoom = _defaultZoom;
            return base.Awake();
        }
        protected override void OnDestroy()
        {
            _showButton.onValueChanged.RemoveListener( OnShowButtonClicked );
            _toggleToolButton.onValueChanged.RemoveListener( OnToggleToolButtonClicked );
            _resetButton.onClick.RemoveListener( OnResetButtonClicked );
            _hFlipButton.onClick.RemoveListener( OnHFlipButtonClicked );
            _vFlipButton.onClick.RemoveListener( OnVFlipButtonClicked );
            _randomButton.onClick.RemoveListener( OnRandomButtonClicked );
            _toggleBackgroundButton.onValueChanged.RemoveListener( OnToggleBackgroundButtonClicked );
            _zoomInButton.onClick.RemoveListener( OnZoomInButtonClicked );
            _zoomOutButton.onClick.RemoveListener( OnZoomOutButtonClicked );
        }

        private void OnShowButtonClicked( bool value )
        {
            _overlayField.SetActive( value );
            showStatus = value;
            _showButtonImage.sprite = value ? openEyeIcon : closeEyeIcon;
        }
        private void OnToggleToolButtonClicked( bool value )
        {
            _toolButtonField.SetActive( value );
            toolStatus = value;
        }
        private void OnResetButtonClicked()
        {
            // * Reset camera
            _characterCamera.orthographicSize = _defaultZoom;
            EventBus.Instance.Publish(new ResetPartArg() );
        }
        private void OnHFlipButtonClicked()
        {
            _characterRenderer.localScale = new Vector3( _characterRenderer.localScale.x.Negative(), _characterRenderer.localScale.y, _characterRenderer.localScale.z );
        }
        private void OnVFlipButtonClicked()
        {
            _characterRenderer.localScale = new Vector3( _characterRenderer.localScale.x, _characterRenderer.localScale.y.Negative(), _characterRenderer.localScale.z );
        }
        private void OnRandomButtonClicked()
        {
            EventBus.Instance.Publish( new ChangePartRandomlyArg() );
        }
        private void OnToggleBackgroundButtonClicked( bool value )
        {
            _backgroundField.SetActive( !value );
            _scrollBackgroundField.SetActive( value );
            backgroundStatus = value;
        }
        private void OnZoomInButtonClicked()
        {
            currentZoom -= _zoomSpeed;
            currentZoom = Mathf.Clamp( currentZoom, _minZoom, _maxZoom );
            _characterCamera.orthographicSize = currentZoom;
        }
        private void OnZoomOutButtonClicked()
        {
            currentZoom += _zoomSpeed;
            currentZoom = Mathf.Clamp( currentZoom, _minZoom, _maxZoom );
            _characterCamera.orthographicSize = currentZoom;
        }
    }
}
