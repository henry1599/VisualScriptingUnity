using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor.VersionControl;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "AnimationDatabase", menuName = "CharacterStudio/Animation Database")]
    public class AnimationDatabase : ScriptableObject
    {
        public List<string> Paths;
        [SerializedDictionary("Animation", "Data")]
        public SerializedDictionary<eCharacterAnimation, AnimationData> Data;
        [Button("Load Animations")]
        public void LoadAnimations()
        {
            // Load all folder in path, the path is from Assets, so must use AssetDatabase, each folder has name as the Animation Name (E.g Idle, Walk, Run, Jump, Attack, Die, Hurt), then load all textures in that folder, After that, save all textures to AnimationData
            // The path has the following format: path/Idle/Body/Body1.png, Body2.png
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
