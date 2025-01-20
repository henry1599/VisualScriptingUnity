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
            DontDestroyOnLoad(gameObject);
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
            ValidateData();
            Save();
        }
        public void AddCustomPart( eCharacterPart part, string path )
        {
            if ( SaveData == null )
            {
                Load();
            }
            if ( SaveData.CustomParts.DataDict.TryGetValue( part, out UserResourceData val ) )
            {
                if ( !val.Paths.Contains( path ) )
                {
                    val.Paths.Add( path );
                }
            }
            else
            {
                SaveData.CustomParts.DataDict[ part ] = new UserResourceData
                {
                    Paths = new List<string> { path }
                };
            }
        }
        public void RemoveCustomPart(eCharacterPart part, string path)
        {
            if ( SaveData == null )
            {
                Load();
            }
            if ( SaveData.CustomParts.DataDict.TryGetValue( part, out UserResourceData val ) )
            {
                val.Paths.Remove( path );
            }
        }
        public void ValidateData()
        {
            foreach ( var part in SaveData.CustomParts.DataDict.Keys )
            {
                SaveData.CustomParts.DataDict[ part ].Paths.RemoveAll( x => !File.Exists( x ) );
            }
        }
        public void InitConfigs()
        {
            CharacterDatabase.LoadExternalCategories();
            CharacterDatabase.LoadExternalData();
            CharacterDatabase.LoadSortedData();
            CharacterDatabase.LoadDefaultParts();
            AnimationDatabase.LoadExternalData();
            MapDatabase.LoadExternalData();
        }
    }
}
