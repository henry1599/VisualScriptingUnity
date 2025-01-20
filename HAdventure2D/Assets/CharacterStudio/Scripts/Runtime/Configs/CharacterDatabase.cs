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
        public Dictionary<eCharacterPart, CategoryData> Categories = new Dictionary<eCharacterPart, CategoryData>();
        public Dictionary<eCharacterPart, CharacterData> Data = new Dictionary<eCharacterPart, CharacterData>();
        public Dictionary<eCharacterPart, int> SortedData; // stored as json
        public Dictionary<eCharacterPart, string> DefaultParts; // stored as json
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
                    return Data[part].TextureDict[DefaultParts[part]].Texture;
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
        public void LoadSortedData()
        {
            SortedData = new SerializedDictionary<eCharacterPart, int>();
            string json = Path.Combine(
                DataManager.Instance.SaveData.DataFolderPath,
                "SortedDataList.json"
            );
            if ( !string.IsNullOrEmpty( json ) )
            {
                var data = new SortedDataList();
                if ( data.FromJsonPath( json ) )
                {
                    foreach ( var item in data.SortedData )
                    {
                        SortedData.TryAdd( item.Part, item.Order );
                    }
                }
            }
        }
        public void LoadDefaultParts()
        {
            DefaultParts = new SerializedDictionary<eCharacterPart, string>();
            string json = Path.Combine(
                DataManager.Instance.SaveData.DataFolderPath,
                "DefaultPartDataList.json"
            );
            if ( !string.IsNullOrEmpty( json ) )
            {
                var data = new DefaultPartDataList();
                if ( data.FromJsonPath( json ) )
                {
                    foreach ( var item in data.DefaultParts )
                    {
                        DefaultParts.Add( item.Part, item.DefaultPart );
                    }
                }
            }
        }
        public void LoadExternalData()
        {
            Data = new Dictionary<eCharacterPart, CharacterData>();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var part in allParts)
            {
                string rootPathFolder = DataManager.Instance.SaveData.DataFolderPath;
                string partPath = Path.Combine(rootPathFolder, part.ToString());
                CharacterData characterData = new CharacterData
                {
                    TextureDict = new Dictionary<string, CSIFileData>()
                };
                if (Directory.Exists(partPath))
                {
                    string[] files = Directory.GetFiles(partPath);
                    foreach (var file in files)
                    {
                        if (file.EndsWith(".csi"))
                        {
                            string id = Path.GetFileNameWithoutExtension(file);
                            CSIFileData texData = CSIFile.LoadCsiFile( file );
                            texData.Texture.filterMode = FilterMode.Point;
                            characterData.TextureDict.TryAdd(id, texData);
                        }
                    }
                    Data.TryAdd(part, characterData);
                }
            }
        }
        public void LoadExternalCategories()
        {
            Categories = new Dictionary<eCharacterPart, CategoryData>();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var part in allParts)
            {
                CategoryData categoryData = new CategoryData();
                string rootPathFolder = DataManager.Instance.SaveData.DataFolderPath;
                string categoryFilePath = Path.Combine(rootPathFolder, "Core", "Categories", part.ToString() + ".csi");
                if (File.Exists(categoryFilePath))
                {
                    if (categoryFilePath.EndsWith(".csi"))
                    {
                        CSIFileData texData = CSIFile.LoadCsiFile( categoryFilePath );
                        texData.Texture.filterMode = FilterMode.Point;

                        categoryData.Icon = texData.Texture;
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
        }
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
        public Dictionary<string, CSIFileData> TextureDict;
    }
}
