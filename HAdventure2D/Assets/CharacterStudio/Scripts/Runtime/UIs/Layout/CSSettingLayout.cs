using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSSettingLayout : CSLayout
    {
        public CSSettingPanel settingPanel;
        public override eLayoutType LayoutType => eLayoutType.Setting;

        public override void Setup()
        {
            settingPanel.Setup();
        }

        public override void Unsetup()
        {
            try
            {
                settingPanel.Unsetup();
            }
            catch (System.Exception)
            {
            }
        }
    }
}
