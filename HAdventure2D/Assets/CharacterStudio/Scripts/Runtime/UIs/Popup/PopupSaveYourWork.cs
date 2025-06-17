using AYellowpaper.SerializedCollections;
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
    [Serializable]
    public class ProgressSaveData
    {
        public List<eCharacterPart> Parts;
        public List<string> Selections;
        public static ProgressSaveData ToProgress(SerializedDictionary<eCharacterPart, string> selections)
        {
            ProgressSaveData progress = new ProgressSaveData
            {
                Parts = new List<eCharacterPart>(),
                Selections = new List<string>()
            };
            foreach (var kvp in selections)
            {
                progress.Parts.Add(kvp.Key);
                progress.Selections.Add(kvp.Value);
            }
            return progress;
        }
        public SerializedDictionary<eCharacterPart, string> ToSelections()
        {
            SerializedDictionary<eCharacterPart, string> selections = new SerializedDictionary<eCharacterPart, string>();
            for (int i = 0; i < Parts.Count; i++)
            {
                selections[Parts[i]] = Selections[i];
            }
            return selections;
        }
    }
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
#if UNITY_EDITOR
            var savePath = DataManager.Instance.DataConfig.GetSaveLoadFolderPath();
            var selection = ProgressSaveData.ToProgress(CharacterAnimation.Instance.CharacterSelection);
            Texture2D icon = CharacterAnimation.Instance.GenerateIcon();
            icon.filterMode = FilterMode.Point; 
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
                AssetDatabase.Refresh();
                // Set filter mode to Point using TextureImporter
                string assetIconPath = iconPath.Substring(iconPath.IndexOf("Assets"));
                TextureImporter importer = AssetImporter.GetAtPath(assetIconPath) as TextureImporter;
                if (importer != null)
                {
                    importer.filterMode = FilterMode.Point;
                    importer.SaveAndReimport();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save character selection: {e.Message}");
            }

            EventBus.Instance.Publish(new HidePopupArg(PopupType));
#endif
        }
    }
}
