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
        [SerializeField] Image iconImage;
        [SerializeField] Button button;
        eCharacterPart part;
        string id = string.Empty;
        public void SetupCategory(Texture2D icon, eCharacterPart part)
        {
            Rect rect = GetIconRect(icon, ICON_SIZE);
            this.iconImage.sprite = Sprite.Create(icon, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = string.Empty;
            this.button.onClick.AddListener(OnClicked);
        }
        public void SetupId(Texture2D icon, eCharacterPart part, string id)
        {
            Rect rect = GetIconRect(icon, ICON_SIZE);
            this.iconImage.sprite = Sprite.Create(icon, rect, new Vector2(0.5f, 0.5f));
            this.part = part;
            this.id = id;
            this.button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            EventBus.Instance.Publish(new ItemClickArg(part, id));
        }


        Rect GetIconRect(Texture2D icon, int iconSize)
        {
            if (iconSize % 2 != 0)
            {
                iconSize++;
            }
            return new Rect((icon.width - iconSize) / 2, (icon.height - iconSize) / 2, iconSize, iconSize);
        }
    }
}
