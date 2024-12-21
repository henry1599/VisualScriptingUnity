using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "CharacterStudio/Character Database")]
    public class CharacterDatabase : ScriptableObject
    {
        public string CatgoryIconPath;
        public List<string> Paths;
        [SerializedDictionary("Part", "Data")]
        public SerializedDictionary<eCharacterPart, CharacterData> Data;
#if UNITY_EDITOR
        [Button("Load Data")]
        public void LoadData()
        {
            Data = new SerializedDictionary<eCharacterPart, CharacterData>();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var path in Paths)
            {
                foreach (var part in allParts)
                {
                    CharacterData characterData = new CharacterData();
                    characterData.TextureDict = new SerializedDictionary<string, Texture2D>();
                    string categoryFilePath = Application.dataPath + CatgoryIconPath + part.ToString() + ".png";
                    if (File.Exists(categoryFilePath))
                    {
                        string pathFromAssets = "Assets" + categoryFilePath.Substring(Application.dataPath.Length);
                        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(pathFromAssets);
                        characterData.CategoryIcon = tex;
                    }
                    string rootPathFolder = Application.dataPath + path + part.ToString() + "/Data/";
                    if (Directory.Exists(rootPathFolder))
                    {
                        string[] files = Directory.GetFiles(rootPathFolder);
                        foreach (var file in files)
                        {
                            if (file.EndsWith(".png"))
                            {
                                string pathFromAssets = "Assets" + file.Substring(Application.dataPath.Length);
                                string id = Path.GetFileNameWithoutExtension(file);
                                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(pathFromAssets);
                                characterData.TextureDict.TryAdd(id, tex);
                            }
                        }
                        Data.TryAdd(part, characterData);
                    }
                }
            }
        }
#endif
    }
    [Serializable]
    public class CharacterData
    {
        public Texture2D CategoryIcon;
        [SerializedDictionary("Id", "Texture")]
        public SerializedDictionary<string, Texture2D> TextureDict;
    }
}
