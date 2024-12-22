using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace CharacterStudio
{
    public class AnimationPanel : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown _animationDropdown;
        [SerializeField] AnimationDatabase _animationDatabase;
        public void SetupAnimationData()
        {
            List<string> animations = _animationDatabase.Data.Keys.Select(x => x.ToString()).ToList();
            _animationDropdown.ClearOptions();
            _animationDropdown.AddOptions(animations);
        }
        void Awake()
        {
            SetupAnimationData();
        }
    }
}
