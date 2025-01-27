using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class UIItem : MonoBehaviour
    {
        protected const int ICON_SIZE = 32;
        [SerializeField] protected Image _iconImage;
        [SerializeField] protected Button _button;
        [SerializeField] protected Button _removeButton;
        [SerializeField] protected Color _lockColor;
        [SerializeField] protected Image _lockIconImage;
        [SerializeField] protected Toggle _locKButtonToggle;

        [Header( "Background" )]
        [SerializeField] protected Image _backgroundImage;
        [SerializeField] protected Color _defaultColor;
        [SerializeField] protected Color _selectedColor;
        protected eCharacterPart part;
        protected string id = string.Empty;
        bool isCategory = false;

        public bool IsLock {get; private set;} = false;
        public eCharacterPart Part => part;

        protected EventSubscription<PartChangedArg> _itemClickSubscription;
        public void SetupCategory( Texture2D icon, eCharacterPart part, TooltipData tooltip)
        {
            _locKButtonToggle.gameObject.SetActive( true );
            _removeButton.gameObject.SetActive( false );
            Rect rect = CSUtils.GetIconRect( icon, ICON_SIZE);
            icon.filterMode = FilterMode.Point;
            this._iconImage.sprite = Sprite.Create( icon, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = string.Empty;
            this.isCategory = true;
            this._button.onClick.AddListener(OnClicked);
            this._locKButtonToggle.onValueChanged.AddListener( OnLockButtonClicked );

            Tooltipable tooltipable = gameObject.GetComponent<Tooltipable>();
            if (tooltipable == null)
            {
                tooltipable = gameObject.AddComponent<Tooltipable>();
            }
            tooltipable.Data = tooltip;
        }
        public virtual void SetupId( CSIFileData csiData, eCharacterPart part, string id, bool selected = false)
        {
            _locKButtonToggle.gameObject.SetActive( false );
            _removeButton.gameObject.SetActive( !csiData.IsDefault );
            Rect rect = CSUtils.GetIconRect( csiData.Texture, ICON_SIZE);
            csiData.Texture.filterMode = FilterMode.Point;
            this._iconImage.sprite = Sprite.Create( csiData.Texture, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = id;
            this.isCategory = false;
            this._button.onClick.AddListener(OnClicked);
            this._removeButton.onClick.AddListener( OnRemoveClicked );

            Color color = selected ? _selectedColor : _defaultColor;
            _backgroundImage.color = color;
        }

        protected virtual void OnLockButtonClicked(bool isOn)
        {
            Color color = isOn ? _lockColor : Color.white;
            _lockIconImage.color = color;
            IsLock = isOn;
        }

        protected void OnRemoveClicked()
        {

        }

        protected void Awake()
        {
            _itemClickSubscription = EventBus.Instance.Subscribe<PartChangedArg>( OnChangePart );
        }
        protected void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _itemClickSubscription );
        }

        protected void OnChangePart( PartChangedArg arg )
        {
            if ( isCategory )
                return;
            if ( arg.Part != part )
                return;
            bool isClickOnThisItem = arg.Id == id;
            Color color = isClickOnThisItem ? _selectedColor : _defaultColor;
            _backgroundImage.color = color;
        }

        public void OnClicked()
        {
            EventBus.Instance.Publish(new ItemClickArg(part, id));
        }
    }
}
