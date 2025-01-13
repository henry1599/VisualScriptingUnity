using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "MapDatabase", menuName = "CharacterStudio/Map Database")]
    public class MapDatabase : ScriptableObject
    {
        public Dictionary<eCharacterPart, Texture2D> Data = new Dictionary<eCharacterPart, Texture2D>();
        public void LoadExternalData()
        {
            Data = new Dictionary<eCharacterPart, Texture2D>();
            foreach (eCharacterPart part in Enum.GetValues(typeof(eCharacterPart)))
            {
                string path = Path.Combine(
                    DataManager.Instance.SaveData.DataFolderPath,
                    "Core",
                    "BaseTextures",
                    part.ToString() + ".csi"
                );
                if (File.Exists(path))
                {
                    Texture2D texture = CSIFile.LoadCsiFile( path );
                    texture.filterMode = FilterMode.Point;
                    Data[part] = texture;
                }
            }
        }
    }
}
