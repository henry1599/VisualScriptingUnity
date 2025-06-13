using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "DataConfig", menuName = "CharacterStudio/Configs/Data Config")]
    public class DataConfig : ScriptableObject
    {
        public DefaultAsset[] DataFolder;
        public DefaultAsset ExportedFolder;
        public int SelectedIndex = 0;
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
    }
}
