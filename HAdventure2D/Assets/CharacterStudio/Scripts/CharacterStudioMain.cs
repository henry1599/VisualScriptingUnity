using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CharacterStudio
{
    public enum eItemType
    {
        Category,
        Item
    }
    public enum eStudioState
    {
        Category,
        Item
    }
    // Features List:
    // - Choose a list of animations of specific items on the character
    // - 
    public class CharacterStudioMain : MonoBehaviour
    {
        [SerializeField] private MapDatabase _mapDatabase;
        [SerializeField] private AnimationDatabase _animationDatabase;
        [SerializeField] private List<eCharacterPart> _availableParts;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Transform itemContainer;
        private List<GameObject> itemObjects = new List<GameObject>();
        private eStudioState _studioState;
        
    }
}
