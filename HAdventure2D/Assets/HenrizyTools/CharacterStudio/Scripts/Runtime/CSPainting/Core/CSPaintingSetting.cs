using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterStudio
{
    [Serializable]
    public class BrushDict
    {
        [Serializable]
        public class BrushData
        {
            public eBrushType BrushType;
            public CSBrush Brush;
            public Sprite BrushIcon;
            public Texture2D BrushCursor;
        }

        [SerializeField]
        public List<BrushData> BrushDataList = new List<BrushData>();
        public List<CSBrush> Values => BrushDataList.Select(b => b.Brush).ToList();
        public CSBrush GetBrush(eBrushType brushType)
        {
            var brushData = BrushDataList.FirstOrDefault(b => b.BrushType == brushType);
            if (brushData != null)
            {
                return brushData.Brush;
            }
            Debug.LogError($"Brush of type {brushType} not found");
            return null;
        }
        public Sprite GetBrushIcon(eBrushType brushType)
        {
            var brushData = BrushDataList.FirstOrDefault(b => b.BrushType == brushType);
            if (brushData != null)
            {
                return brushData.BrushIcon;
            }
            Debug.LogError($"Brush icon of type {brushType} not found");
            return null;
        }
        public Texture2D GetBrushCursor(eBrushType brushType)
        {
            var brushData = BrushDataList.FirstOrDefault(b => b.BrushType == brushType);
            if (brushData != null)
            {
                return brushData.BrushCursor;
            }
            Debug.LogError($"Brush cursor of type {brushType} not found");
            return null;
        }
        public bool TryGetBrush(eBrushType brushType, out CSBrush brush)
        {
            brush = GetBrush(brushType);
            return brush != null;
        }
        public bool TryGetBrushIcon(eBrushType brushType, out Sprite brushIcon)
        {
            brushIcon = GetBrushIcon(brushType);
            return brushIcon != null;
        }
        public bool TryGetBrushCursor(eBrushType brushType, out Texture2D brushCursor)
        {
            brushCursor = GetBrushCursor(brushType);
            return brushCursor != null;
        }
    }
    [CreateAssetMenu(fileName = "CSPaintingSetting", menuName = "CharacterStudio/CSPaintingSetting", order = 0)]
    public class CSPaintingSetting : ScriptableObject
    {
        public int BackgroundTileSize = 16;
        public Color BackgroundTileColor1 = new Color(0.8f, 0.8f, 0.8f, 1f);
        public Color BackgroundTileColor2 = new Color(0.6f, 0.6f, 0.6f, 1f);
        public eBrushType DefaultBrush;
        public List<Color> DefaultPalette;
        public BrushDict BrushData;
        public Texture2D GetBrushCursor(eBrushType brushType)
        {
            if (BrushData == null)
            {
                Debug.LogError("BrushCursorsDict is not set");
                return null;
            }
            if (!BrushData.TryGetBrushCursor(brushType, out Texture2D brushCursor))
            {
                Debug.LogError("BrushCursor is not found");
                return null;
            }
            return brushCursor;
        }
        public Sprite GetBrushIcon(eBrushType brushType)
        {
            if (BrushData == null)
            {
                Debug.LogError("BrushIconsdict is not set");
                return null;
            }
            if (!BrushData.TryGetBrushIcon(brushType, out Sprite brushIcon))
            {
                Debug.LogError("BrushIcon is not found");
                return null;
            }
            return brushIcon;
        }
        public CSBrush GetBrush(eBrushType brushType)
        {
            if (BrushData == null)
            {
                Debug.LogError("Brushes is not set");
                return null;
            }
            if (!BrushData.TryGetBrush(brushType, out CSBrush brush))
            {
                Debug.LogError("Brush is not found");
                return null;
            }
            return brush;
        }
        public List<CSBrush> GetAllBrushes()
        {
            if (BrushData == null)
            {
                Debug.LogError("Brushes is not set");
                return null;
            }
            return BrushData.Values.ToList();
        }
    }
}
