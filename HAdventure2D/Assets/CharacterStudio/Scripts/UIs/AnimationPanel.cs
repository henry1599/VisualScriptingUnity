using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

namespace CharacterStudio
{
    public class AnimationPanel : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown _animationDropdown;
        [SerializeField] AnimationDatabase _animationDatabase;
        List<eCharacterAnimation> _animationList;
        public void SetupAnimationData()
        {
            _animationList = new List<eCharacterAnimation>(_animationDatabase.Data.Keys);
            List<string> animations = _animationList.Select(x => x.ToString()).ToList();
            _animationDropdown.ClearOptions();
            _animationDropdown.AddOptions(animations);

            _animationDropdown.onValueChanged.AddListener(OnAnimationChanged);
        }

        private void OnAnimationChanged(int index)
        {
            eCharacterAnimation animation = _animationList[index];
            EventBus.Instance.Publish(new ChangeAnimationArg(animation));
        }

        void Awake()
        {
            SetupAnimationData();
        }
    }
}
