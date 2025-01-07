using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class UserSaveData
    {
        public string DataFolderPath;
        public byte[] ToBinary()
        {
            return System.Text.Encoding.UTF8.GetBytes( DataFolderPath );
        }
        public string SaveToPath( string path )
        {
            System.IO.File.WriteAllBytes( path, ToBinary() );
            return path;
        }
        public static UserSaveData LoadFromPath( string path )
        {
            UserSaveData data = new UserSaveData();
            byte[] bytes = System.IO.File.ReadAllBytes( path );
            data.DataFolderPath = System.Text.Encoding.UTF8.GetString( bytes );
            return data;
        }
    }
}
