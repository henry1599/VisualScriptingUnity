using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPaintingLayout : CSLayout
    {
        public override eLayoutType LayoutType => eLayoutType.Painting;

        public override void Setup()
        {
            CSPaintingManager.Instance?.Setup();
        }

        public override void Unsetup()
        {
            try
            {
                CSPaintingManager.Instance?.Unsetup();
            }
            catch (System.Exception)
            {
            }
        }
    }
}
