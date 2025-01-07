using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace CharacterStudio
{
    public enum eLayoutType
    {
        None,
        Character,
        Painting,
        Setting
    }
    public abstract class CSLayout : MonoBehaviour
    {
        [ShowNativeProperty, SerializeField] public abstract eLayoutType LayoutType { get; }
        public abstract void Setup();
        public abstract void Unsetup();
    }
}
