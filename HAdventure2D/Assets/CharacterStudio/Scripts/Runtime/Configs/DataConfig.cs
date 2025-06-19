using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CharacterStudio
{
    [Serializable]
    public class AnimationNameData
    {
        public string Id;
        public string DisplayName;
    }
    [CreateAssetMenu(fileName = "DataConfig", menuName = "CharacterStudio/Configs/Data Config")]
    public class DataConfig : ScriptableObject
    {
        public AnimationNameData[] AnimationNames;

#if UNITY_EDITOR
        public DefaultAsset[] DataFolder;
        public DefaultAsset ExportedFolder;
        public DefaultAsset SaveLoadFolder;
#endif
        public int SelectedIndex = 0;
        public string GetDisplayName(string id)
        {
            if (AnimationNames == null || AnimationNames.Length == 0)
            {
                return string.Empty;
            }
            foreach (var animationName in AnimationNames)
            {
                if (animationName.Id == id)
                {
                    return animationName.DisplayName;
                }
            }
            return string.Empty;
        }
#if UNITY_EDITOR
        public string GetSaveLoadFolderPath()
        {
            if (SaveLoadFolder == null)
            {
                Debug.LogWarning("SaveLoad folder is not set.");
                return null;
            }
            string path = AssetDatabase.GetAssetPath(SaveLoadFolder);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("SaveLoad folder path is empty.");
                return null;
            }
            if (!AssetDatabase.IsValidFolder(path))
            {
                Debug.LogWarning("SaveLoad folder path is not a valid folder.");
                return null;
            }
            return path;
        }
        public string GetExportedFolderPath()
        {
            if (ExportedFolder == null)
            {
                Debug.LogWarning("Exported folder is not set.");
                return null;
            }
            string path = AssetDatabase.GetAssetPath(ExportedFolder);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Exported folder path is empty.");
                return null;
            }
            if (!AssetDatabase.IsValidFolder(path))
            {
                Debug.LogWarning("Exported folder path is not a valid folder.");
                return null;
            }
            return path;
        }
        public string GetFolderPath()
        {
            if (DataFolder == null || DataFolder.Length == 0)
            {
                Debug.LogWarning("Data folder is not set or empty.");
                return null;
            }
            if (SelectedIndex < 0 || SelectedIndex >= DataFolder.Length)
            {
                Debug.LogWarning("Selected index is out of bounds.");
                return null;
            }
            string path = AssetDatabase.GetAssetPath(DataFolder[SelectedIndex]);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Data folder path is empty.");
                return null;
            }
            if (!AssetDatabase.IsValidFolder(path))
            {
                Debug.LogWarning("Data folder path is not a valid folder.");
                return null;
            }
            return path;
        }
#endif
    }
}
