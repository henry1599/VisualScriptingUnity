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
        public int Size = 32;
        [SerializedDictionary("Part", "Category Data")]
        public SerializedDictionary<eCharacterPart, CategoryData> Categories;
        [SerializedDictionary("Part", "Character Data")]
        public SerializedDictionary<eCharacterPart, CharacterData> Data
        {
            get
            {
                if (_data == null)
                {
                    LoadExternal();
                }
                return _data;
            }
        } SerializedDictionary<eCharacterPart, CharacterData> _data;
        [SerializedDictionary("Part", "Sorted Data")]
        public SerializedDictionary<eCharacterPart, int> SortedData;
        [SerializedDictionary( "Part", "Default Part" )]
        public SerializedDictionary<eCharacterPart, string> DefaultParts;
        public string GetCategoryDisplayName(eCharacterPart part)
        {
            if (Categories.ContainsKey(part))
            {
                return Categories[part].DisplayName;
            }
            return part.ToString();
        }
        public Texture2D GetDefaultPartTexture(eCharacterPart part)
        {
            if (DefaultParts.ContainsKey(part))
            {
                if (Data.ContainsKey(part) && Data[part].TextureDict.ContainsKey(DefaultParts[part]))
                {
                    return Data[part].TextureDict[DefaultParts[part]];
                }
            }
            return null;
        }
        public List<eCharacterPart> GetSortedPart(List<eCharacterPart> parts)
        {
            return parts.OrderBy(x => SortedData[x]).ToList();
        }
        public string GetRandomId( eCharacterPart part )
        {
            if ( Data.ContainsKey( part ) )
            {
                var keys = Data[ part ].TextureDict.Keys.ToList();
                return keys[ UnityEngine.Random.Range( 0, keys.Count ) ];
            }
            return string.Empty;
        }
        public List<(string id, eCharacterPart part)> GetRandomAll()
        {
            List<(string id, eCharacterPart part)> result = new List<(string id, eCharacterPart part)>();
            foreach ( var part in Data.Keys )
            {
                var keys = Data[ part ].TextureDict.Keys.ToList();
                result.Add( (keys[ UnityEngine.Random.Range( 0, keys.Count ) ], part) );
            }
            return result;
        }
        public bool IsValid( eCharacterPart part, string id )
        {
            if ( Data.ContainsKey( part ) )
            {
                return Data[ part ].TextureDict.ContainsKey( id );
            }
            return false;
        }
        public void LoadExternal()
        {
            _data = new SerializedDictionary<eCharacterPart, CharacterData>();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var part in allParts)
            {
                string rootPathFolder = DataManager.Instance.SaveData.DataFolderPath;
                string partPath = Path.Combine(rootPathFolder, part.ToString());
                CharacterData characterData = new CharacterData
                {
                    TextureDict = new SerializedDictionary<string, Texture2D>()
                };
                if (Directory.Exists(partPath))
                {
                    string[] files = Directory.GetFiles(partPath);
                    foreach (var file in files)
                    {
                        if (file.EndsWith(".png"))
                        {
                            string id = Path.GetFileNameWithoutExtension(file);
                            byte[] fileData = File.ReadAllBytes(file);
                            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                            tex.LoadImage(fileData);
                            tex.filterMode = FilterMode.Point;
                            characterData.TextureDict.TryAdd(id, tex);
                        }
                    }
                    _data.TryAdd(part, characterData);
                }
            }
        }
#if UNITY_EDITOR
        [Button("Load Data")]
        public void LoadData()
        {
            Categories = new SerializedDictionary<eCharacterPart, CategoryData>();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var part in allParts)
            {
                CategoryData categoryData = new CategoryData();
                string categoryFilePath = Application.dataPath + CatgoryIconPath + part.ToString() + ".png";
                if (File.Exists(categoryFilePath))
                {
                    string pathFromAssets = "Assets" + categoryFilePath.Substring(Application.dataPath.Length);
                    Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(pathFromAssets);
                    categoryData.Icon = tex;
                    categoryData.Part = part;
                    categoryData.DisplayName = part.ToString();
                    if (Categories.ContainsKey(part))
                    {
                        Categories[part].Part = part;
                        Categories[part].Icon = categoryData.Icon;
                    }
                    else
                    {
                        Categories.TryAdd(part, categoryData);
                    }
                }
            }
        }
#endif
    }
    [Serializable]
    public class CategoryData
    {
        [HideInInspector] public eCharacterPart Part;
        public string DisplayName;
        public Texture2D Icon;
    }
    [Serializable]
    public class CharacterData
    {
        [SerializedDictionary("Id", "Texture")]
        public SerializedDictionary<string, Texture2D> TextureDict;
    }
}
