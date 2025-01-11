using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NaughtyAttributes;
using UnityEngine;

namespace CharacterStudio
{
    public class CSJson : ScriptableObject
    {
        public virtual string ToJson()
        {
            return JsonUtility.ToJson( this );
        }
        public virtual bool FromJson( string json )
        {
            try
            {
                JsonUtility.FromJsonOverwrite( json, this );
                return true;
            }
            catch ( System.Exception e )
            {
                Debug.LogErrorFormat( "Failed to load from json type {0}", this.GetType());
                return false;
            }
        }
        public virtual bool FromJsonPath( string jsonPath )
        {
            try
            {
                var json = System.IO.File.ReadAllText( jsonPath );
                FromJson( json );
                return true;
            }
            catch ( System.Exception e )
            {
                Debug.LogErrorFormat( "Failed to load from json type {0}", this.GetType() );
                return false;
            }
        }
#if UNITY_EDITOR
            [Button]
        public void SaveJson()
        {
            var json = ToJson();
            var path = UnityEditor.EditorUtility.SaveFilePanel( "Save Json", "", name + ".json", "json" );
            if ( !string.IsNullOrEmpty( path ) )
            {
                System.IO.File.WriteAllText( path, json );
            }
            Debug.Log( "Saved to " + path );
        }
        [Button]
        public void LoadJson()
        {
            var path = UnityEditor.EditorUtility.OpenFilePanel( "Load Json", "", "json" );
            if ( !string.IsNullOrEmpty( path ) )
            {
                var json = System.IO.File.ReadAllText( path );
                FromJson( json );
            }
            Debug.Log( "Loaded from " + path );
        }
#endif
    }
}
