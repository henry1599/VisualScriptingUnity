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
        [SerializeField] public abstract eLayoutType LayoutType { get; }
        public abstract void Setup();
        public abstract void Unsetup();
    }
}
