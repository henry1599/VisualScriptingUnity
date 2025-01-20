using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [Serializable]
    public class UserSaveData
    {
        public string DataFolderPath = "";
        public UserResourceDataDict CustomParts = new UserResourceDataDict();

        public void Save()
        {
            PlayerPrefs.SetString( DataManager.SAVEDATA_KEY, ToJson() );
        }
        public static UserSaveData Load()
        {
            string json = PlayerPrefs.GetString( DataManager.SAVEDATA_KEY, "" );
            if ( string.IsNullOrEmpty( json ) )
            {
                return new UserSaveData();
            }
            return FromJson( json );
        }
        public string ToJson()
        {
            return JsonUtility.ToJson( this );
        }
        public static UserSaveData FromJson( string json )
        {
            return JsonUtility.FromJson<UserSaveData>( json );
        }
    }
    [Serializable]
    public class UserResourceDataDict
    {
        public Dictionary<eCharacterPart, UserResourceData> DataDict;
        public UserResourceDataDict()
        {
            DataDict = new Dictionary<eCharacterPart, UserResourceData>();
        }
    }
    [Serializable]
    public class UserResourceData
    {
        public List<string> Paths;
        public UserResourceData()
        {
            Paths = new List<string>();
        }
    }
}
