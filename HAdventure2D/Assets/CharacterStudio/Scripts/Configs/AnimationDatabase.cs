using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "AnimationDatabase", menuName = "CharacterStudio/Animation Database")]
    public class AnimationDatabase : ScriptableObject
    {
        public string AnimationRootPath = "Assets/CharacterStudio/Sprites/Legacy/Animations";
        [SerializedDictionary("Animation", "Data")]
        public SerializedDictionary<eCharacterAnimation, AnimationData> Data;
        public List<Texture2D> GetAnimations(eCharacterAnimation animationType, eCharacterPart part, string id)
        {
            if (!Data.ContainsKey(animationType))
            {
                return null;
            }
            if (!Data[animationType].Animation.ContainsKey(part))
            {
                return null;
            }
            if (!Data[animationType].Animation[part].Ids.ContainsKey(id))
            {
                return null;
            }
            if (!TryGetPath(animationType, part, id, out string path))
            {
                return null;
            }
            List<Texture2D> textures = new List<Texture2D>();
            AssetDatabase.LoadAllAssetsAtPath(path).ToList().ForEach(asset =>
            {
                if (asset is Texture2D)
                {
                    textures.Add(asset as Texture2D);
                }
            });
            return textures;
        }
        public string GeneratePath(eCharacterAnimation animationType, eCharacterPart part, string id)
        {
            return Path.Combine(AnimationRootPath, animationType.ToString(), part.ToString(), Data[animationType].Animation[part].Ids[id]);
        }
        public bool PathExists(eCharacterAnimation animationType, eCharacterPart part, string id)
        {
            return Directory.Exists(GeneratePath(animationType, part, id));
        }
        public bool TryGetPath(eCharacterAnimation animationType, eCharacterPart part, string id, out string path)
        {
            path = GeneratePath(animationType, part, id);
            return PathExists(animationType, part, id);
        }
    }
    [Serializable]
    public class AnimationData
    {
        [SerializedDictionary("Part", "Animation Folder")]
        public SerializedDictionary<eCharacterPart, IDs> Animation;
    }
    [Serializable]
    public class IDs
    {
        [SerializedDictionary("ID", "Folder")]
        public SerializedDictionary<string, string> Ids;
    }
}
