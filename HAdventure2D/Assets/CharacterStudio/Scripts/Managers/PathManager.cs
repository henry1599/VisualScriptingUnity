using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CharacterStudio
{
    public class PathManager : MonoSingleton<PathManager>
    {
        public readonly string SAVE_DATA_FILE_NAME = "UserSaveData.csd";
        public readonly string SAVE_DATA_FOLDER_NAME = "SaveData";
        public string GetSaveDataFolderPath()
        {
            string saveFolder = Path.Combine( Application.dataPath, SAVE_DATA_FOLDER_NAME );
            if ( !Directory.Exists( saveFolder ) )
            {
                Directory.CreateDirectory( saveFolder );
            }
            return saveFolder;
        }
        public string GetSaveDataFilePath()
        {
            string savePath = Path.Combine( GetSaveDataFolderPath(), SAVE_DATA_FILE_NAME );
            return savePath;
        }
    }
}
