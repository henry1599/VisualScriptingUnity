using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CharacterStudio
{
    [CreateAssetMenu(fileName = "CSPaintingSetting", menuName = "CharacterStudio/CSPaintingSetting", order = 0)]
    public class CSPaintingSetting : ScriptableObject
    {
        public int BackgroundTileSize = 16;
        public Color BackgroundTileColor1 = new Color(0.8f, 0.8f, 0.8f, 1f);
        public Color BackgroundTileColor2 = new Color(0.6f, 0.6f, 0.6f, 1f);
        public eBrushType DefaultBrush;
        public SerializedDictionary<eBrushType, Sprite> BrushIconsDict;
        public SerializedDictionary<eBrushType, Texture2D> BrushCursorsDict;
        public Texture2D GetBrushCursor(eBrushType brushType)
        {
            if (BrushCursorsDict == null)
            {
                Debug.LogError("BrushCursorsDict is not set");
                return null;
            }
            if (!BrushCursorsDict.TryGetValue(brushType, out Texture2D brushCursor))
            {
                Debug.LogError("BrushCursor is not found");
                return null;
            }
            return brushCursor;
        }
        public Sprite GetBrushIcon(eBrushType brushType)
        {
            if (BrushIconsDict == null)
            {
                Debug.LogError("BrushIconsdict is not set");
                return null;
            }
            if (!BrushIconsDict.TryGetValue(brushType, out Sprite brushIcon))
            {
                Debug.LogError("BrushIcon is not found");
                return null;
            }
            return brushIcon;
        }
    }
}
