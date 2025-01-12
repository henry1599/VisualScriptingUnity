using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSCharacterLayout : CSLayout
    {
        [SerializeField] Button _settingButton;
        public override eLayoutType LayoutType => eLayoutType.Character;

        public override void Setup()
        {
            _settingButton.onClick.AddListener(OnClickSetting);
            CharacterStudioMain.Instance?.Setup();
        }

        private void OnClickSetting()
        {
            EventBus.Instance.Publish(new OnChangeLayoutArg(eLayoutType.Setting));
        }

        public override void Unsetup()
        {
            _settingButton.onClick.RemoveAllListeners();
            try
            {
                CharacterStudioMain.Instance?.Unsetup();
            }
            catch (System.Exception)
            {
            }
        }
    }
}
