using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class OverlayManager : MonoSingleton<OverlayManager>
    {
        [SerializeField] GameObject _overlayField;
        [SerializeField] Camera _characterCamera;



        [Header("TOP")]
        [SerializeField] Toggle _showButton;
        [SerializeField] Image _showButtonImage;


        [Header("LEFT")]
        [SerializeField] Toggle _toggleToolButton;
        [SerializeField] Image _toggleToolButtonImage;
        [SerializeField] Button _resetButton;
        [SerializeField] Button _hFlipButton;
        [SerializeField] Button _vFlipButton;
        [SerializeField] Button _randomButton;
        [SerializeField] Toggle _toggleBackgroundButton;
        [SerializeField] Image _toggleBackgroundButtonImage;
        [SerializeField] GameObject _toolButtonField;
        [SerializeField] GameObject _backgroundField;
        [SerializeField] GameObject _scrollBackgroundField;
        [SerializeField] Transform _characterRenderer;

        [Header("RIGHT")]
        [SerializeField] Button _zoomInButton;
        [SerializeField] Button _zoomOutButton;
        [SerializeField] Button _speedupButton;
        [SerializeField] TMP_Text _speedText;


        [Header("SETTING")]
        [SerializeField] float _zoomSpeed = 0.1f;
        [SerializeField] float _defaultZoom = 0.32f;
        [SerializeField] float _minZoom = 0.12f;
        [SerializeField] float _maxZoom = 0.62f;
        [SerializeField] Sprite openEyeIcon;
        [SerializeField] Sprite closeEyeIcon;




        private float currentZoom;
        private bool showStatus = true;
        private bool toolStatus = false;
        private bool backgroundStatus = true;

        protected override bool Awake()
        {
            _showButton.onValueChanged.AddListener(OnShowButtonClicked);
            _toggleToolButton.onValueChanged.AddListener(OnToggleToolButtonClicked);
            _resetButton.onClick.AddListener(OnResetButtonClicked);
            _hFlipButton.onClick.AddListener(OnHFlipButtonClicked);
            _vFlipButton.onClick.AddListener(OnVFlipButtonClicked);
            _randomButton.onClick.AddListener(OnRandomButtonClicked);
            _toggleBackgroundButton.onValueChanged.AddListener(OnToggleBackgroundButtonClicked);
            _zoomInButton.onClick.AddListener(OnZoomInButtonClicked);
            _zoomOutButton.onClick.AddListener(OnZoomOutButtonClicked);
            _speedupButton.onClick.AddListener(OnSpeedupButtonClicked);


            _showButton.isOn = showStatus;
            _toggleToolButton.isOn = toolStatus;
            _toggleBackgroundButton.isOn = backgroundStatus;


            currentZoom = _defaultZoom;
            return base.Awake();
        }


        protected override void OnDestroy()
        {
            _showButton.onValueChanged.RemoveListener(OnShowButtonClicked);
            _toggleToolButton.onValueChanged.RemoveListener(OnToggleToolButtonClicked);
            _resetButton.onClick.RemoveListener(OnResetButtonClicked);
            _hFlipButton.onClick.RemoveListener(OnHFlipButtonClicked);
            _vFlipButton.onClick.RemoveListener(OnVFlipButtonClicked);
            _randomButton.onClick.RemoveListener(OnRandomButtonClicked);
            _toggleBackgroundButton.onValueChanged.RemoveListener(OnToggleBackgroundButtonClicked);
            _zoomInButton.onClick.RemoveListener(OnZoomInButtonClicked);
            _zoomOutButton.onClick.RemoveListener(OnZoomOutButtonClicked);
            _speedupButton.onClick.RemoveListener(OnSpeedupButtonClicked);
        }
        private void OnSpeedupButtonClicked()
        {
            DataManager.Instance.AnimationDatabase.ToggleSpeed();
            string speedStr = DataManager.Instance.AnimationDatabase.GetAnimationText();
            _speedText.text = $"{speedStr}";
            CharacterAnimation.Instance.UpdateInterval();
        }

        private void OnShowButtonClicked(bool value)
        {
            _overlayField.SetActive(value);
            showStatus = value;
            _showButtonImage.sprite = value ? openEyeIcon : closeEyeIcon;
        }
        private void OnToggleToolButtonClicked(bool value)
        {
            _toolButtonField.SetActive(value);
            toolStatus = value;
        }
        private void OnResetButtonClicked()
        {
            // * Reset camera
            _characterCamera.orthographicSize = _defaultZoom;
            EventBus.Instance.Publish(new ResetPartArg());
        }
        private void OnHFlipButtonClicked()
        {
            _characterRenderer.localScale = new Vector3(_characterRenderer.localScale.x.Negative(), _characterRenderer.localScale.y, _characterRenderer.localScale.z);
        }
        private void OnVFlipButtonClicked()
        {
            _characterRenderer.localScale = new Vector3(_characterRenderer.localScale.x, _characterRenderer.localScale.y.Negative(), _characterRenderer.localScale.z);
        }
        private void OnRandomButtonClicked()
        {
            EventBus.Instance.Publish(new ChangePartRandomlyArg());
        }
        private void OnToggleBackgroundButtonClicked(bool value)
        {
            _backgroundField.SetActive(!value);
            _scrollBackgroundField.SetActive(value);
            backgroundStatus = value;
        }
        private void OnZoomInButtonClicked()
        {
            currentZoom -= _zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, _minZoom, _maxZoom);
            _characterCamera.orthographicSize = currentZoom;
        }
        public void SetDefaultZoom()
        {
            currentZoom = _defaultZoom;
            _characterCamera.orthographicSize = currentZoom;
        }
        private void OnZoomOutButtonClicked()
        {
            currentZoom += _zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, _minZoom, _maxZoom);
            _characterCamera.orthographicSize = currentZoom;
        }
    }
}
