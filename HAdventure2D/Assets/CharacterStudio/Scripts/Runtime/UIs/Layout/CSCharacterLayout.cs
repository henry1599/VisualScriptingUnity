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
            CharacterStudioMain.Instance?.Setup();
        }
        public override void Unsetup()
        {
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
