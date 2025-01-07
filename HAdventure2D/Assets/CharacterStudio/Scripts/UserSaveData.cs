using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class UserSaveData
    {
        public string DataFolderPath = "";
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
}
