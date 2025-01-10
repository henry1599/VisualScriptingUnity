using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "PopupConfig", menuName = "CharacterStudio/Configs/PopupConfig")]
    public class PopupConfig : ScriptableObject
    {
        [SerializedDictionary("Type", "Prefab")]
        public SerializedDictionary<ePopupType, GameObject> PopupPrefabs;
    }
}
