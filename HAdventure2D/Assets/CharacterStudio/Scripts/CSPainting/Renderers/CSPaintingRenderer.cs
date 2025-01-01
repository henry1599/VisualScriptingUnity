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
            _drawingTexture = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGBA32, false)
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
        public Color GetColorAtIndex(int x, int y)
        {
            return _pixelColors[ y * _renderTexture.width + x ];
        }
        public Color GetColorAtIndex(Vector2Int index)
        {
            return GetColorAtIndex(index.x, index.y);
        }
        public Color GetColorAtIndex(int i)
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
    }
}
