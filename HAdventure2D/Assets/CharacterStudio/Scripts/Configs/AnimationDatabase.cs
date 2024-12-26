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
        [SerializedDictionary("Animation", "Data")]
        public SerializedDictionary<eCharacterAnimation, AnimationData> Data;
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
#if UNITY_EDITOR
        [Button("Load Animations")]
        public void LoadAnimations()
        {
            Data = new SerializedDictionary<eCharacterAnimation, AnimationData>();
            List<eCharacterAnimation> allAnimations = Enum.GetValues(typeof(eCharacterAnimation)).Cast<eCharacterAnimation>().ToList();
            List<eCharacterPart> allParts = Enum.GetValues(typeof(eCharacterPart)).Cast<eCharacterPart>().ToList();
            foreach (var path in Paths)
            {
                foreach (var anim in allAnimations)
                {
                    AnimationData data = new AnimationData();
                    data.AnimationsByPart = new SerializedDictionary<eCharacterPart, AnimationDataList>();
                    foreach (var part in allParts)
                    {
                        string rootPathFolder = Application.dataPath + path + anim.ToString() + "/" + part.ToString() + "/";
                        if (Directory.Exists(rootPathFolder))
                        {
                            data.AnimationsByPart[part] = new AnimationDataList();
                            data.AnimationsByPart[part].Textures = new List<Texture2D>();
                            string[] files = Directory.GetFiles(rootPathFolder);
                            foreach (var file in files)
                            {
                                if (file.EndsWith(".png"))
                                {
                                    string pathFromAssets = "Assets" + file.Substring(Application.dataPath.Length);
                                    Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(pathFromAssets);
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
#endif
    }
    [Serializable]
    public class AnimationData
    {
        [SerializedDictionary("Part", "Data")]
        public SerializedDictionary<eCharacterPart, AnimationDataList> AnimationsByPart;
    }
    [Serializable]
    public class AnimationDataList
    {
        public List<Texture2D> Textures;
    }
}
