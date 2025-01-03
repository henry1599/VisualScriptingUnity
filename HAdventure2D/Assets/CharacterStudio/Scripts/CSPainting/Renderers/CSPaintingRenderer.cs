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
    }
}
