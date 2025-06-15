using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PopupSaveYourWork : PopupBase
    {
        [SerializeField] Button _saveButton;
        public override ePopupType PopupType => ePopupType.Save_Your_Work;
        public override void Show()
        {
            base.Show();
            _saveButton.onClick.AddListener(OnSaveButtonClicked);
        }

        private void OnSaveButtonClicked()
        {
            var savePath = DataManager.Instance.DataConfig.GetSaveLoadFolderPath();
            var selection = CharacterAnimation.Instance.CharacterSelection;
            Texture2D icon = CharacterAnimation.Instance.GenerateIcon();
            string json = JsonUtility.ToJson(selection);
            if (string.IsNullOrEmpty(savePath))
            {
                Debug.LogError("Save path is not valid.");
                return;
            }
            string folderName = $"{_name.text}";
            string folderPath = Path.Join(savePath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = $"{_name.text}.json";
            string filePath = Path.Join(folderPath, fileName);
            string iconName = $"{_name.text}.png";
            string iconPath = Path.Join(folderPath, iconName);

            try
            {
                File.WriteAllText(filePath, json);
                File.WriteAllBytes(iconPath, icon.EncodeToPNG());
                Debug.Log($"Saved character selection to {filePath} and icon to {iconPath}");
                EventBus.Instance.Publish(new HidePopupArg(PopupType));
                //EventBus.Instance.Publish(new SaveSuccessArg());
#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save character selection: {e.Message}");
            }
        }
    }
}
