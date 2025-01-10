using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSCharacterLayout : CSLayout
    {
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
