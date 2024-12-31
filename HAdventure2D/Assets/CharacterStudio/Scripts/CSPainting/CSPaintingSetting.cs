using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "CSPaintingSetting", menuName = "CharacterStudio/CSPaintingSetting", order = 0)]
    public class CSPaintingSetting : ScriptableObject
    {
        public int BackgroundTileSize = 16;
        public Color BackgroundTileColor1 = new Color(0.8f, 0.8f, 0.8f, 1f);
        public Color BackgroundTileColor2 = new Color(0.6f, 0.6f, 0.6f, 1f);
    }
}
