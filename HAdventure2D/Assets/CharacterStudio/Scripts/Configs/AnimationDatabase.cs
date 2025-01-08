using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "AnimationDatabase", menuName = "CharacterStudio/Animation Database")]
    public class AnimationDatabase : ScriptableObject
    {
        public List<string> Paths;
        public float AnimationInterval = 0.15f;
        public Dictionary<eCharacterAnimation, AnimationData> Data;
        public List<Texture2D> GetAnimations(eCharacterAnimation animation, eCharacterPart part)
        {
            if (Data.ContainsKey(animation))
            {
                if (Data[animation].AnimationsByPart.ContainsKey(part))
                {
                    return Data[animation].AnimationsByPart[part].Textures;
                }
            }
            return null;
        }
        public int GetAnimationFrameCount(eCharacterAnimation animation)
        {
            if (Data.ContainsKey(animation))
            {
                return Data[animation].AnimationsByPart.FirstOrDefault().Value.Textures.Count;
            }
            return 0;
        }
        public void LoadExternalData()
        {
            Data = new Dictionary<eCharacterAnimation, AnimationData>();
            List<eCharacterAnimation> allAnimations = Enum.GetValues(typeof(eCharacterAnimation)).Cast<eCharacterAnimation>().ToList();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var anim in allAnimations)
            {
                AnimationData data = new AnimationData
                {
                    AnimationsByPart = new Dictionary<eCharacterPart, AnimationDataList>()
                };
                foreach (var part in allParts)
                {
                    string rootPathFolder = Path.Combine(
                        DataManager.Instance.SaveData.DataFolderPath,
                        "Core",
                        "AnimationMap",
                        anim.ToString(),
                        part.ToString()
                    );
                    if (Directory.Exists(rootPathFolder))
                    {
                        data.AnimationsByPart[part] = new AnimationDataList
                        {
                            Textures = new List<Texture2D>()
                        };
                        string[] files = Directory.GetFiles(rootPathFolder);
                        foreach (var file in files)
                        {
                            if (file.EndsWith(".png"))
                            {
                                byte[] fileData = File.ReadAllBytes(file);
                                Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                                tex.LoadImage(fileData);
                                tex.filterMode = FilterMode.Point;
                                data.AnimationsByPart[part].Textures.Add(tex);
                            }
                        }
                        if (Data.ContainsKey(anim))
                        {
                            Data[anim] = data;
                        }
                        else
                        {
                            Data.Add(anim, data);
                        }
                    }
                }
            }
        }
    }
    [Serializable]
    public class AnimationData
    {
        public Dictionary<eCharacterPart, AnimationDataList> AnimationsByPart;
    }
    [Serializable]
    public class AnimationDataList
    {
        public List<Texture2D> Textures;
    }
}
