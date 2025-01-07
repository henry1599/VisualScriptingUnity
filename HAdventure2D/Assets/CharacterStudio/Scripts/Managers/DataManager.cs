using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CharacterStudio
{
    public class DataManager : MonoSingleton<DataManager>
    {
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
            string savePath = PathManager.Instance.GetSaveDataFilePath();
            SaveData.SaveToPath( savePath );
        }
        public void Load()
        {
            string savePath = PathManager.Instance.GetSaveDataFilePath();
            if ( File.Exists( savePath ) )
            {
                SaveData = UserSaveData.LoadFromPath( savePath );
            }
        }
    }
}
