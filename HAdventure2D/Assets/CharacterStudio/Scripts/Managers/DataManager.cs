using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CharacterStudio
{
    public class DataManager : MonoSingleton<DataManager>
    {
        public static readonly string SAVEDATA_KEY = "U53rS4v3D4t4";
        public UserSaveData SaveData { get; private set; } = null;
        protected override bool Awake()
        {
            Load();
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
