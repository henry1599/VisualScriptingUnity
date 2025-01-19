using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterStudio
{
    public static class CSUtils
    {
        public static int GetFileCount( string folderPath, string extension )
        {
            string[] files = System.IO.Directory.GetFiles( folderPath, extension, System.IO.SearchOption.AllDirectories );
            return files.Length;
        }
        public static T SafeAddComponent<T>( this GameObject go ) where T : Component
        {
            T component = go.GetComponent<T>();
            if ( component == null )
            {
                component = go.AddComponent<T>();
            }
            return component;
        }
        public static Rect GetIconRect(Texture2D icon, int iconSize)
        {
            if (iconSize % 2 != 0)
            {
                iconSize++;
            }
            return new Rect((icon.width - iconSize) / 2, (icon.height - iconSize) / 2, iconSize, iconSize);
        }
        public static Dictionary<Color32, Color32> LoadMappedColors(Texture2D baseMap, Texture2D baseTexture)
        {
            Dictionary<Color32, Color32> result = new Dictionary<Color32, Color32>();
            for (int x = 0; x < baseMap.width; x++)
            {
                for (int y = 0; y < baseMap.height; y++)
                {
                    Color32 baseMapColor = baseMap.GetPixel(x, y);
                    if (baseMapColor.a == 0)
                    {
                        continue;
                    }
                    Color32 baseTextureColor = baseTexture.GetPixel(x, y);
                    result.TryAdd(baseMapColor, baseTextureColor);
                }
            }
            return result;
        }
        public static Texture2D GenerateTexture(Texture2D baseMap , Dictionary<Color32, Color32> map)
        {
            Texture2D newTexture = new Texture2D(baseMap.width, baseMap.height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };
            for (int x = 0; x < baseMap.width; x++)
            {
                for (int y = 0; y < baseMap.height; y++)
                {
                    Color32 baseMapColor = baseMap.GetPixel(x, y);
                    if (baseMapColor.a == 0)
                    {
                        newTexture.SetPixel(x, y, new Color32(0, 0, 0, 0));
                        continue;
                    }
                    if (map.TryGetValue(baseMapColor, out Color32 baseTextureColor))
                    {
                        // baseTextureColor.a = 255;
                        newTexture.SetPixel(x, y, baseTextureColor);
                    }
                    else
                    {
                        newTexture.SetPixel(x, y, new Color32(0, 0, 0, 0));
                    }
                }
            }
            newTexture.name = baseMap.name;
            newTexture.Apply();
            return newTexture;
        }

        internal static void SaveTexture(Texture2D generatedTexture, string path, string fileName)
        {
            byte[] bytes = generatedTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes(path + "/" + fileName + ".png", bytes);
        }
        internal static int Negative(this int value )
        {
            return -value;
        }
        internal static float Negative( this float value )
        {
            return -value;
        }
        internal static Vector2Int GetPixelIndex( Vector2 normalizedPixelPosition, RenderTexture rt)
        {
            int x = Mathf.Clamp((int)(normalizedPixelPosition.x * rt.width), 0, rt.width - 1);
            int y = Mathf.Clamp((int)(normalizedPixelPosition.y * rt.height), 0, rt.height - 1);
            return new Vector2Int(x, y);
        }
        internal static Vector2 GetNormalizedPositionOnPaintingCanvas( PointerEventData eventData, RectTransform rectTransform )
        {
            if ( RectTransformUtility.ScreenPointToLocalPointInRectangle( rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPosition ) )
            {
                Vector2 normalizedPosition = ProcessPosition( localPosition, rectTransform?.rect.width ?? 1080, rectTransform?.rect.height ?? 1080 );
                return normalizedPosition;
            }
            return Vector2.zero;
        }
        
        internal static Vector2 ProcessPosition( Vector2 localMousePosition, float width, float height )
        {
            Vector2 normalizedPosition = NormalizePixelPosition( localMousePosition, width, height );
            return normalizedPosition;
        }

        internal static Vector2 NormalizePixelPosition( Vector2 pixelPosition, float width, float height )
        {
            float normalizedX = Mathf.InverseLerp( 0, width, pixelPosition.x + width / 2f );
            float normalizedY = Mathf.InverseLerp( 0, height, pixelPosition.y + height / 2f );
            return new( normalizedX, normalizedY );
        }
    }
}
