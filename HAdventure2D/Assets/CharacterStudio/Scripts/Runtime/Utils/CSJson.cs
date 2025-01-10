using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSJson
    {
        public virtual string ToJson()
        {
            return JsonUtility.ToJson( this );
        }
        public virtual void FromJson( string json )
        {
            JsonUtility.FromJsonOverwrite( json, this );
        }
    }
}
