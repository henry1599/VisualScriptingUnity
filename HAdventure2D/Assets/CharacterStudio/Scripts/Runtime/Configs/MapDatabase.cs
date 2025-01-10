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
                    part.ToString() + ".png"
                );
                if (File.Exists(path))
                {
                    byte[] fileData = File.ReadAllBytes(path);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    Data[part] = texture;
                }
            }
        }
    }
}
