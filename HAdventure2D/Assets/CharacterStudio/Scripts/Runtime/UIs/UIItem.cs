using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class UIItem : MonoBehaviour
    {
        private const int ICON_SIZE = 32;
        [SerializeField] Image _iconImage;
        [SerializeField] Button _button;
        [SerializeField] Button _removeButton;

        [Header( "Background" )]
        [SerializeField] Image _backgroundImage;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        eCharacterPart part;
        string id = string.Empty;

        EventSubscription<PartChangedArg> _itemClickSubscription;
        public void SetupCategory( Texture2D icon, eCharacterPart part, TooltipData tooltip)
        {
            _removeButton.gameObject.SetActive( false );
            Rect rect = CSUtils.GetIconRect( icon, ICON_SIZE);
            icon.filterMode = FilterMode.Point;
            this._iconImage.sprite = Sprite.Create( icon, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = string.Empty;
            this._button.onClick.AddListener(OnClicked);

            Tooltipable tooltipable = gameObject.GetComponent<Tooltipable>();
            if (tooltipable == null)
            {
                tooltipable = gameObject.AddComponent<Tooltipable>();
            }
            tooltipable.Data = tooltip;
        }
        public void SetupId( CSIFileData csiData, eCharacterPart part, string id, bool selected = false)
        {
            _removeButton.gameObject.SetActive( !csiData.IsDefault );
            Rect rect = CSUtils.GetIconRect( csiData.Texture, ICON_SIZE);
            csiData.Texture.filterMode = FilterMode.Point;
            this._iconImage.sprite = Sprite.Create( csiData.Texture, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = id;
            this._button.onClick.AddListener(OnClicked);
            this._removeButton.onClick.AddListener( OnRemoveClicked );

            Color color = selected ? _selectedColor : _defaultColor;
            _backgroundImage.color = color;
        }

        private void OnRemoveClicked()
        {

        }

        private void Awake()
        {
            _itemClickSubscription = EventBus.Instance.Subscribe<PartChangedArg>( OnChangePart );
        }
        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _itemClickSubscription );
        }

        private void OnChangePart( PartChangedArg arg )
        {
            if ( arg.Part != part )
                return;
            bool isClickOnThisItem = arg.Id == id;
            Color color = isClickOnThisItem ? _selectedColor : _defaultColor;
            _backgroundImage.color = color;
        }

        private void OnClicked()
        {
            EventBus.Instance.Publish(new ItemClickArg(part, id));
        }
    }
}
