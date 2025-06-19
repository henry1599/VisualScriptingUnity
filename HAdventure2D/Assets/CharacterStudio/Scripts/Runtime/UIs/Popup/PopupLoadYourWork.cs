using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PopupLoadYourWork : PopupBase
    {
        public override ePopupType PopupType => ePopupType.Load_Your_Work;
        public UISaveItem itemPrefab;
        public Transform itemContainer;
        private List<(string name, ProgressSaveData data)> _saveDataList = new();
        private UISaveItem _selectedItem = null;
        public Button _confirmButton;
        private void Start()
        {
            UISaveItem.OnItemSelected += HandleSelection;
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }
        private void OnDestroy()
        {
            UISaveItem.OnItemSelected -= HandleSelection;
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
        }

        private void OnConfirmButtonClicked()
        {
            ProgressSaveData selectedData = _selectedItem.SaveData;
            SerializedDictionary<eCharacterPart, string> selections = selectedData.ToSelections();
            CharacterAnimation.Instance.SetupFromSelection(selections);
            EventBus.Instance.Publish(new HidePopupArg(PopupType));
        }

        private void HandleSelection(UISaveItem item)
        {
            _selectedItem = item;
            _confirmButton.interactable = _selectedItem != null;
        }

        public override void Show()
        {
            base.Show();
            Load();
        }
        void DeleteData(string folderName)
        {
#if UNITY_EDITOR
            string folderPath = DataManager.Instance.DataConfig.GetSaveLoadFolderPath();
            string fullPath = System.IO.Path.Combine(folderPath, folderName);
            AssetDatabase.DeleteAsset(fullPath);
            AssetDatabase.Refresh();
#endif
        }
        void Load()
        {
#if UNITY_EDITOR
            string folderPath = DataManager.Instance.DataConfig.GetSaveLoadFolderPath();
            string[] subFolderPath = System.IO.Directory.GetDirectories(folderPath);
            // Get a sprite and a json in each subfolder
            foreach (string path in subFolderPath)
            {
                string folderName = System.IO.Path.GetFileName(path);
                string[] files = System.IO.Directory.GetFiles(path);
                Sprite icon = null;
                string json = null;
                foreach (string file in files)
                {
                    if (file.EndsWith(".png"))
                    {
                        icon = AssetDatabase.LoadAssetAtPath<Sprite>(file);
                    }
                    else if (file.EndsWith(".json"))
                    {
                        json = System.IO.File.ReadAllText(file);
                    }
                }
                if (icon != null && json != null)
                {
                    // Create a UISaveItem and setup with the icon and json
                    UISaveItem saveItem = Instantiate(itemPrefab, itemContainer);
                    ProgressSaveData saveData = JsonUtility.FromJson<ProgressSaveData>(json);
                    saveItem.Setup(icon, folderName, saveData, DeleteData);
                    if (saveData == null)
                    {
                        Debug.LogError($"Failed to load save data from {json}");
                        continue;
                    }
                    _saveDataList.Add((folderName, saveData));
                }
            }
#endif
        }
    }
}
