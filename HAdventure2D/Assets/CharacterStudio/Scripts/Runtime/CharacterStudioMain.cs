using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using NaughtyAttributes;
using System;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

namespace CharacterStudio
{
    public enum eItemType
    {
        Category,
        Item
    }
    public enum eStudioState
    {
        Category,
        Item
    }
    public class CharacterStudioMain : MonoSingleton<CharacterStudioMain>
    {
        [Header("DATABASE")]
        [SerializeField] private AnimationPanel _animationPanel;
        [Space(5)]


        [Header("UI")]
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] private List<eCharacterPart> _availableParts;
        [SerializeField] private List<eCharacterPart> _emptyPartsAsStart;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private UIItem _itemPrefab;
        [SerializeField] private UIItem _removeItemPrefab;
        [SerializeField] private Button _rightPanelBackButton;
        [SerializeField] private Button _addNewPartButton;
        [ReadOnly, SerializeField] List<eCharacterPart> _actualCategories = new List<eCharacterPart>();
        private eStudioState _studioState;
        private eCharacterPart _selectedCategory;
        private List<UIItem> _partItems = new List<UIItem>();
        private EventSubscription<ItemClickArg> _itemClickSubscription;
        public bool IsSetup {get; private set;}

        public eCharacterPart SelectedCategory => _selectedCategory;

        protected override bool Awake()
        {
            return base.Awake();
        }
        public void Setup()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _itemClickSubscription = EventBus.Instance.Subscribe<ItemClickArg>(OnItemClick);
            _addNewPartButton.onClick.AddListener(OnAddNewPartButtonClicked);
            UpdateState(eStudioState.Category);
            ReloadCategories();

            _rightPanelBackButton.onClick.AddListener(OnBackButtonClicked);
            CharacterAnimation.Instance.Setup();
            IsSetup = true;
        }
        public void Unsetup()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            EventBus.Instance.Unsubscribe( _itemClickSubscription );
            _addNewPartButton.onClick.RemoveAllListeners();
            _rightPanelBackButton.onClick.RemoveAllListeners();
            IsSetup = false;
        }
        public List<eCharacterPart> GetLockParts()
        {
            List<eCharacterPart> lockParts = new List<eCharacterPart>();
            foreach (var item in _partItems)
            {
                if (item.IsLock)
                {
                    lockParts.Add(item.Part);
                }
            }
            return lockParts;
        }
        public bool IsEmptyItemAsStarted(eCharacterPart part)
        {
            return _emptyPartsAsStart.Contains(part);
        }
        private void OnAddNewPartButtonClicked()
        {
            eCharacterPart part = _selectedCategory;
            if (part == eCharacterPart.None)
            {
                return;
            }
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            EventBus.Instance.Publish(new OnChangeLayoutArg(eLayoutType.Painting));
        }

        private void OnBackButtonClicked()
        {
            switch (_studioState)
            {
                case eStudioState.Category:
                    break;
                case eStudioState.Item:
                    ReloadCategories();
                    UpdateState(eStudioState.Category);
                    break;
            }
        }

        public void UpdateState(eStudioState state)
        {
            _studioState = state;
            _rightPanelBackButton.gameObject.SetActive(state == eStudioState.Item);
            _addNewPartButton.gameObject.SetActive(state == eStudioState.Item);
        }
        private void OnItemClick(ItemClickArg arg)
        {
            switch (_studioState)
            {
                case eStudioState.Category:
                    ReloadItems(arg.Part);
                    UpdateState(eStudioState.Item);
                    break;
                case eStudioState.Item:
                    EventBus.Instance.Publish(new ChangePartArg(arg.Part, arg.Id));
                    break;
            }
        }
        void ReloadCategories()
        {
            _partItems = new List<UIItem>();
            _selectedCategory = eCharacterPart.None;
            // * Get categories
            var characterDatabaseKeys = DataManager.Instance.CharacterDatabase.Data.Keys;
            _actualCategories = characterDatabaseKeys.Intersect(_availableParts).ToList();

            // * Set title
            _titleText.text = "Categories";

            // * Clear items object
            int count = _itemContainer.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                Destroy(_itemContainer.GetChild(i).gameObject);
            }

            // * Create items
            foreach (var category in _actualCategories)
            {
                UIItem item = Instantiate(_itemPrefab, _itemContainer);
                TooltipData tooltip = new TooltipData()
                {
                    Description = $"Enter {DataManager.Instance.CharacterDatabase.Categories[category].DisplayName} selection"
                };
                item.SetupCategory(DataManager.Instance.CharacterDatabase.Categories[category].Icon, category, tooltip);
                _partItems.Add(item);
            }
        }
        public void ReloadItems(eCharacterPart category)
        {
            _selectedCategory = category;
            // * Set title
            _titleText.text = DataManager.Instance.CharacterDatabase.Categories[category].DisplayName;

            // * Clear items object
            int count = _itemContainer.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                Destroy(_itemContainer.GetChild(i).gameObject);
            }

            UIItem uiRemoveItem = Instantiate(_removeItemPrefab, _itemContainer);
            bool selected = false;
            if (CharacterAnimation.Instance != null)
            {
                selected = _emptyPartsAsStart.Contains(category) && !CharacterAnimation.Instance.CharacterSelection.ContainsKey( category );
            }
            uiRemoveItem.SetupId(null, category, string.Empty, selected );

            // * Create items
            foreach (var item in DataManager.Instance.CharacterDatabase.Data[category].TextureDict)
            {
                UIItem uiItem = Instantiate(_itemPrefab, _itemContainer);
                selected = false;
                if (CharacterAnimation.Instance != null)
                {
                    selected = CharacterAnimation.Instance.CharacterSelection.ContainsKey( category ) && CharacterAnimation.Instance.CharacterSelection[ category ] == item.Key;
                }
                if (_emptyPartsAsStart.Contains(category) && item.Key == string.Empty)
                {
                    selected = false;
                }
                uiItem.SetupId(item.Value, category, item.Key, selected );
            }
        }
    }
}
