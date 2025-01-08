using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class AnimationPanel : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown _animationDropdown;

        [Header("CONTROL")]
        [SerializeField] Toggle _playButton;
        [SerializeField] Image _playButtonImage;
        [SerializeField] Sprite _playSprite;
        [SerializeField] Sprite _pauseSprite;
        [SerializeField] Button _nextFrameButton;
        [SerializeField] Button _prevFrameButton;

        [Header("SLIDER")]
        [SerializeField] RectTransform _sliderParentRect;
        [SerializeField] RectTransform _sliderRect;
        [SerializeField] Slider _animationSlider;
        [SerializeField] Transform _keyFrameBackContainer;
        [SerializeField] Transform _keyFrameFrontContainer;
        [SerializeField] GameObject _keyFrameBack;
        [SerializeField] GameObject _keyFrameFront;


        List<eCharacterAnimation> _animationList;

        EventSubscription<FrameIndexUpdateArg> _frameIndexUpdateSubscription;
        EventSubscription<AnimationUpdateArg> _animationUpdateSubscription;
        public void SetupAnimationData()
        {
            _animationList = new List<eCharacterAnimation>(DataManager.Instance.AnimationDatabase.Data.Keys);
            List<string> animations = _animationList.Select(x => x.ToString()).ToList();
            _animationDropdown.ClearOptions();
            _animationDropdown.AddOptions(animations);

            _animationDropdown.onValueChanged.AddListener(OnAnimationChanged);
        }

        private void OnAnimationChanged(int index)
        {
            eCharacterAnimation animation = _animationList[index];
            EventBus.Instance.Publish(new ChangeAnimationArg(animation));
        }

        void Awake()
        {
            SetupAnimationData();

            _playButton.onValueChanged.AddListener(OnPlayButtonClicked);
            _nextFrameButton.onClick.AddListener(OnNextFrameButtonClicked);
            _prevFrameButton.onClick.AddListener(OnPrevFrameButtonClicked);
            _animationSlider.onValueChanged.AddListener(OnSliderValueChanged);

            _frameIndexUpdateSubscription = EventBus.Instance.Subscribe<FrameIndexUpdateArg>(OnFrameIndexUpdate);
            _animationUpdateSubscription = EventBus.Instance.Subscribe<AnimationUpdateArg>(OnAnimationUpdate);
        }

        private void OnSliderValueChanged(float value)
        {
            int frameIndex = (int)value;
            CharacterAnimation.Instance?.SetFrameIndex(frameIndex);
        }

        private void OnPlayButtonClicked(bool isOn)
        {
            Sprite sprite = isOn ? _pauseSprite : _playSprite;
            _playButtonImage.sprite = sprite;
            if (CharacterAnimation.Instance == null)
            {
                return;
            }
            CharacterAnimation.Instance.IsPlaying = isOn;
            _prevFrameButton.interactable = _nextFrameButton.interactable = !isOn;
        }

        private void OnNextFrameButtonClicked()
        {
            int currentFrameIndex = (int)_animationSlider.value;
            int nextFrameIndex = currentFrameIndex + 1;
            CharacterAnimation.Instance?.SetFrameIndex(nextFrameIndex);
        }

        private void OnPrevFrameButtonClicked()
        {
            int currentFrameIndex = (int)_animationSlider.value;
            int prevFrameIndex = currentFrameIndex - 1;
            CharacterAnimation.Instance?.SetFrameIndex(prevFrameIndex);
        }

        void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(_frameIndexUpdateSubscription);
            EventBus.Instance.Unsubscribe(_animationUpdateSubscription);

            _playButton.onValueChanged.RemoveAllListeners();
            _nextFrameButton.onClick.RemoveAllListeners();
            _prevFrameButton.onClick.RemoveAllListeners();
        }

        private void OnAnimationUpdate(AnimationUpdateArg arg)
        {
            // * Clear children
            int childCount = _keyFrameBackContainer.childCount;
            for (int i = childCount - 1; i >= 1; i--)
            {
                Destroy(_keyFrameBackContainer.GetChild(i).gameObject);
            }
            childCount = _keyFrameFrontContainer.childCount;
            for (int i = childCount - 1; i >= 1; i--)
            {
                Destroy(_keyFrameFrontContainer.GetChild(i).gameObject);
            }

            int frameCount = DataManager.Instance.AnimationDatabase.GetAnimationFrameCount(arg.AnimationType);
            // * Minus to left and right of _sliderRect
            

            float totalWidth = _sliderParentRect.rect.width - Mathf.Abs(_sliderRect.offsetMax.x) - Mathf.Abs(_sliderRect.offsetMin.x) - 10.5f; 
            float fragmentWidth = totalWidth / (frameCount - 1);
            for (int i = 0; i < frameCount - 2; i++)
            {
                GameObject keyFrameBack = Instantiate(_keyFrameBack, _keyFrameBackContainer);
                GameObject keyFrameFront = Instantiate(_keyFrameFront, _keyFrameFrontContainer);

                keyFrameBack.GetComponent<RectTransform>().anchoredPosition = new Vector2(fragmentWidth * (i + 1), 0);
                keyFrameFront.GetComponent<RectTransform>().anchoredPosition = new Vector2(fragmentWidth * (i + 1), 0);

                keyFrameBack.SetActive(true);
                keyFrameFront.SetActive(true);
            }

            _animationSlider.maxValue = frameCount - 1;
        }

        private void OnFrameIndexUpdate(FrameIndexUpdateArg arg)
        {
            _animationSlider.value = arg.FrameIndex;
        }
    }
}
