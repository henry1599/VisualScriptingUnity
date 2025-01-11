using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CharacterStudio
{
    [DefaultExecutionOrder(-200)]
    public class DataManager : MonoSingleton<DataManager>
    {
        [Header("DATABASE")]
        public MapDatabase MapDatabase;
        public AnimationDatabase AnimationDatabase;
        public CharacterDatabase CharacterDatabase;
        [Space(5)]
        public static readonly string SAVEDATA_KEY = "U53rS4v3D4t4";
        public UserSaveData SaveData { get; private set; } = null;
        protected override bool Awake()
        {
            Load();
            
            CharacterDatabase.LoadExternalCategories();
            CharacterDatabase.LoadExternalData();
            CharacterDatabase.LoadSortedData();
            CharacterDatabase.LoadDefaultParts();
            AnimationDatabase.LoadExternalData();
            MapDatabase.LoadExternalData();
            return base.Awake();
        }
        public void Save()
        {
            if ( SaveData == null )
            {
                SaveData = new UserSaveData();
            }
            SaveData.Save();
        }
        public void Load()
        {
            SaveData = UserSaveData.Load();
            Save();
        }
    }
}
