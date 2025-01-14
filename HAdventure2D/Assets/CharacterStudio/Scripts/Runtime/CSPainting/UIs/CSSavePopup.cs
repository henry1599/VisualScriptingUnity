using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSSavePopup : PopupBase
    {
        public override ePopupType PopupType => ePopupType.CSP_Save;
        [SerializeField] TMP_Text _saveInfo;
        [SerializeField] TMP_InputField _nameInputField;
        [SerializeField] Button _saveButton;
        public override void Hide()
        {
            base.Hide();
        }
        public override void Show()
        {
            base.Show();
            _saveButton.onClick.AddListener(OnSaveButtonClicked);

            string categoryName = CSPaintingManager.Instance.GetPartDisplayName();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Save new item to database");
            sb.AppendLine("Category: " + categoryName);
            _saveInfo.text = sb.ToString();
        }

        private void OnSaveButtonClicked()
        {
            var category = CharacterStudioMain.Instance.SelectedCategory;
            string dataFolder = DataManager.Instance.SaveData.DataFolderPath;
            string path = Path.Combine(
                dataFolder,
                category.ToString(),
                _nameInputField.text + ".csi"
            );
            Texture2D texture = CSPaintingManager.Instance.GetPaintingTexture();
            CSIFile.SaveAsCsiFile( texture, path );
            DataManager.Instance.InitConfigs();
            EventBus.Instance.Publish( new HidePopupArg( PopupType ) );
        }
    }
}
