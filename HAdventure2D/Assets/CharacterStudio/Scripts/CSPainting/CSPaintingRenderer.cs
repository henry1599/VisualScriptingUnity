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

        public Texture2D DrawingTexture => _drawingTexture;
        public RenderTexture RT => _renderTexture;
        public Color[] PixelColors => _pixelColors;
        protected virtual void Start()
        {
            // _bgColors = new Color[ _bgRenderTexture.width * _bgRenderTexture.height ];
            // _bgTexture = new Texture2D( _bgRenderTexture.width, _bgRenderTexture.height, TextureFormat.RGBA32, false );
            // _bgTexture.filterMode = FilterMode.Point;

            _drawingTexture = new Texture2D( _renderTexture.width, _renderTexture.height, TextureFormat.RGBA32, false );
            _drawingTexture.filterMode = FilterMode.Point;
            _pixelColors = new Color[ _renderTexture.width * _renderTexture.height ];
            ClearCanvas();

            // InitBackgroundTexture();
        }
        public void ClearCanvas()
        {
            for ( int i = 0; i < _pixelColors.Length; i++ )
            {
                _pixelColors[ i ] = Color.clear;
            }
            UpdateRenderTexture();
        }
        // private void InitBackgroundTexture()
        // {
        //     for ( int y = 0; y < _bgRenderTexture.height; y++ )
        //     {
        //         for ( int x = 0; x < _bgRenderTexture.width; x++ )
        //         {
        //             bool isGrey =   x / BG_BLOCK_SIZE  % 2 ==  y / BG_BLOCK_SIZE  % 2 ;
        //             _bgColors[ y * _bgRenderTexture.width + x ] = isGrey ? bgBlockColor1 : bgBlockColor2;
        //         }
        //     }

        //     _bgTexture.SetPixels( _bgColors );
        //     _bgTexture.Apply();

        //     RenderTexture currentActiveRT = RenderTexture.active;
        //     RenderTexture.active = _bgRenderTexture;
        //     Graphics.Blit( _bgTexture, _bgRenderTexture );
        //     RenderTexture.active = currentActiveRT;
        // }
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
