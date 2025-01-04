using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSPaintingRenderer : MonoBehaviour
    {
        [SerializeField] protected RenderTexture _renderTexture;
        protected Texture2D _drawingTexture;
        protected Color[] _pixelColors;
        protected bool _isSetup = false;

        public Texture2D DrawingTexture => _drawingTexture;
        public RenderTexture RT => _renderTexture;
        public Color[] PixelColors => _pixelColors;
        protected virtual void Start()
        {
            _isSetup = false;
            _drawingTexture = new Texture2D( _renderTexture.width, _renderTexture.height, TextureFormat.RGBA32, false )
            {
                filterMode = FilterMode.Point
            };
            _pixelColors = new Color[ _renderTexture.width * _renderTexture.height ];
            ClearCanvas();
            _isSetup = true;
        }
        protected virtual void OnDestroy()
        {
            ClearCanvas();
        }
        public void ClearCanvas()
        {
            for ( int i = 0; i < _pixelColors.Length; i++ )
            {
                _pixelColors[ i ] = Color.clear;
            }
            UpdateRenderTexture();
        }
        public void ClearCanvasWithouRect( Vector2Int startIndex, Vector2Int endIndex )
        {
            int minX = Mathf.Min( startIndex.x, endIndex.x );
            int maxX = Mathf.Max( startIndex.x, endIndex.x );
            int minY = Mathf.Min( startIndex.y, endIndex.y );
            int maxY = Mathf.Max( startIndex.y, endIndex.y );
            for ( int i = 0; i < _pixelColors.Length; i++ )
            {
                int x = i % _renderTexture.width;
                int y = i / _renderTexture.width;
                if ( x >= minX && x <= maxX && y >= minY && y <= maxY )
                    continue;
                _pixelColors[ i ] = Color.clear;
            }
        }
        public void ClearInRect( Vector2Int startIndex, Vector2Int endIndex )
        {
            int minX = Mathf.Min( startIndex.x, endIndex.x );
            int maxX = Mathf.Max( startIndex.x, endIndex.x );
            int minY = Mathf.Min( startIndex.y, endIndex.y );
            int maxY = Mathf.Max( startIndex.y, endIndex.y );
            for ( int y = minY; y <= maxY; y++ )
            {
                for ( int x = minX; x <= maxX; x++ )
                {
                    int i = y * _renderTexture.width + x;
                    if ( i >= _pixelColors.Length || i < 0 )
                        continue;
                    _pixelColors[ i ] = Color.clear;
                }
            }
            UpdateRenderTexture();
        }
        public Color GetColorAtIndex( int x, int y )
        {
            return _pixelColors[ y * _renderTexture.width + x ];
        }
        public Color GetColorAtIndex( Vector2Int index )
        {
            return GetColorAtIndex( index.x, index.y );
        }
        public Color GetColorAtIndex( int i )
        {
            return _pixelColors[ i ];
        }
        public void UpdateRenderTexture()
        {
            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = _renderTexture;
            _drawingTexture.SetPixels( _pixelColors );
            _drawingTexture.Apply();
            Graphics.Blit( _drawingTexture, _renderTexture );
            RenderTexture.active = currentActiveRT;
        }
        public static void CopyTo( CSPaintingRenderer from, CSPaintingRenderer to )
        {
            for ( int i = 0; i < from.PixelColors.Length; i++ )
            {
                if ( i >= from.PixelColors.Length || i < 0 )
                    continue;
                if ( from.PixelColors[ i ] == Color.clear )
                    continue;
                to.PixelColors[ i ] = from.PixelColors[ i ];
            }
            to.UpdateRenderTexture();
        }
        public static void CopyToRect( CSPaintingRenderer from, CSPaintingRenderer to, Vector2Int startIndex, Vector2Int endIndex )
        {
            int minX = Mathf.Min( startIndex.x, endIndex.x );
            int maxX = Mathf.Max( startIndex.x, endIndex.x );
            int minY = Mathf.Min( startIndex.y, endIndex.y );
            int maxY = Mathf.Max( startIndex.y, endIndex.y );
            for ( int y = minY; y <= maxY; y++ )
            {
                for ( int x = minX; x <= maxX; x++ )
                {
                    int i = y * from.RT.width + x;
                    if ( i >= from.PixelColors.Length || i < 0)
                        continue;
                    if ( from.PixelColors[ i ] == Color.clear )
                        continue;
                    to.PixelColors[ i ] = from.PixelColors[ i ];
                }
            }
            to.UpdateRenderTexture();
        }
        // * Save a rect on a renderer into a separated array
        public static Color[] SaveToArray( CSPaintingRenderer renderer, Vector2Int startIndex, Vector2Int endIndex )
        {
            int minX = Mathf.Min(startIndex.x, endIndex.x);
            int maxX = Mathf.Max(startIndex.x, endIndex.x);
            int minY = Mathf.Min(startIndex.y, endIndex.y);
            int maxY = Mathf.Max(startIndex.y, endIndex.y);

            int countX = maxX - minX + 1;
            int countY = maxY - minY + 1;
            Color[] results = new Color[countX * countY];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = Color.clear;
            }
            for (int y = minY, rY = 0; y <= maxY; y++, rY++)
            {
                for (int x = minX, rX = 0; x <= maxX; x++, rX++)
                {
                    int i = y * renderer.RT.width + x;
                    int rI = rY * countX + rX; // Corrected index calculation
                    if (i >= renderer.PixelColors.Length || i < 0)
                        continue;
                    results[rI] = renderer.PixelColors[i];
                }
            }
            return results;
        }
        public static void LoadArrayToRenderer( CSPaintingRenderer renderer, Color[] pixelArray, Vector2Int startIndex, Vector2Int endIndex )
        {
            int minX = Mathf.Min(startIndex.x, endIndex.x);
            int maxX = Mathf.Max(startIndex.x, endIndex.x);
            int minY = Mathf.Min(startIndex.y, endIndex.y);
            int maxY = Mathf.Max(startIndex.y, endIndex.y);
            int width = renderer.RT.width;
            int height = renderer.RT.height;

            int countX = maxX - minX + 1;
            int countY = maxY - minY + 1;

            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    int resultY = startIndex.y + y - countY + 1;
                    int resultX = startIndex.x + x;

                    if (resultX < 0 || resultX > renderer.DrawingTexture.width)
                        continue;

                    int i = resultY * width + resultX;
                    int rI = y * countX + x;
                    if (i >= renderer.PixelColors.Length || i < 0)
                        continue;
                    renderer.PixelColors[i] = pixelArray[rI];
                }
            }
            renderer.UpdateRenderTexture();
        }
    }
}
