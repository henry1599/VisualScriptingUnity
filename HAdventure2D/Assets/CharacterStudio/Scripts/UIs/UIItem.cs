using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class UIItem : MonoBehaviour
    {
        private const int ICON_SIZE = 24;
        [SerializeField] Image _iconImage;
        [SerializeField] Button _button;

        [Header( "Background" )]
        [SerializeField] Image _backgroundImage;
        [SerializeField] Color _defaultColor;
        [SerializeField] Color _selectedColor;
        eCharacterPart part;
        string id = string.Empty;

        EventSubscription<PartChangedArg> _itemClickSubscription;
        public void SetupCategory(Texture2D icon, eCharacterPart part, TooltipData tooltip)
        {
            Rect rect = CSUtils.GetIconRect(icon, ICON_SIZE);
            this._iconImage.sprite = Sprite.Create(icon, rect, new Vector2(0.5f, 0.5f));
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
        public void SetupId( Texture2D icon, eCharacterPart part, string id, bool selected = false)
        {
            Rect rect = CSUtils.GetIconRect(icon, ICON_SIZE);
            this._iconImage.sprite = Sprite.Create(icon, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = id;
            this._button.onClick.AddListener(OnClicked);

            Color color = selected ? _selectedColor : _defaultColor;
            _backgroundImage.color = color;
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
