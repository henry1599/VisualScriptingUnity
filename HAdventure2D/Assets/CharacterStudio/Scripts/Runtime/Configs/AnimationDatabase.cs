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
        private readonly (string displayText, float multiplier)[] ANIMATION_INTERVALS = new (string displayText, float multiplier)[] {
            ("1", 1f),
            ("2", 0.5f),
            ("4", 0.25f),
        };
        public float BaseAnimationSpeed = 0.1f;
        public Dictionary<eCharacterAnimation, AnimationData> Data;
        private int index = 0;
        private void Awake() 
        {
            index = 0;
        }
        public void ToggleSpeed()
        {
            index = (index + 1) % ANIMATION_INTERVALS.Length;
        }
        public float GetAnimationInterval()
        {
            if (index < 0 || index >= ANIMATION_INTERVALS.Length)
            {
                index = 0;
            }
            return ANIMATION_INTERVALS[index].multiplier * BaseAnimationSpeed;
        }
        public string GetAnimationText()
        {
            return ANIMATION_INTERVALS[index].displayText;
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
                        // Sort files by the number right behind the part name
                        Array.Sort(files, (x, y) =>
                        {
                            int xNumber = ExtractNumberFromFileName(x);
                            int yNumber = ExtractNumberFromFileName(y);
                            return xNumber.CompareTo(yNumber);
                        });
                        foreach (var file in files)
                        {
                            if (file.EndsWith(".csi"))
                            {
                                Texture2D tex = CSIFile.LoadCsiFile( file );
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
        private int ExtractNumberFromFileName(string fileName)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string numberPart = new string(nameWithoutExtension.SkipWhile(c => !char.IsDigit(c)).TakeWhile(char.IsDigit).ToArray());
            return int.TryParse(numberPart, out int number) ? number : 0;
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
