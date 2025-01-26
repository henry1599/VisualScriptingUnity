using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class UIRemoveItem : UIItem
    {
        public override void SetupId( CSIFileData csiData, eCharacterPart part, string id, bool selected = false)
        {
            this.part = part;
            this.id = id;
            this._button.onClick.AddListener(OnClicked);

            Color color = selected ? _selectedColor : _defaultColor;
            _backgroundImage.color = color;
        }
    }
}
