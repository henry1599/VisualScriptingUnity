using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "MapDatabase", menuName = "CharacterStudio/Map Database")]
    public class MapDatabase : ScriptableObject
    {
        [SerializedDictionary("Part", "Data")]
        public SerializedDictionary<eCharacterPart, Texture2D> Data;
    }
}
