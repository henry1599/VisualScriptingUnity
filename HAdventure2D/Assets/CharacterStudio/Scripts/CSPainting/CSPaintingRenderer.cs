using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSPaintingRenderer : MonoBehaviour
    {
        private const int BG_BLOCK_SIZE = 8;
        [SerializeField] Color bgBlockColor1;
        [SerializeField] Color bgBlockColor2;
        [SerializeField] RenderTexture _renderTexture;
        [SerializeField] RenderTexture _bgRenderTexture;
        Texture2D _drawingTexture;
        Texture2D _bgTexture;
        Color[] _pixelColors;
        Color[] _bgColors;

        public Texture2D DrawingTexture => _drawingTexture;
        private void Start()
        {
            _bgColors = new Color[ _bgRenderTexture.width * _bgRenderTexture.height ];
            _bgTexture = new Texture2D( _bgRenderTexture.width, _bgRenderTexture.height, TextureFormat.RGBA32, false );
            _bgTexture.filterMode = FilterMode.Point;

            _drawingTexture = new Texture2D( _renderTexture.width, _renderTexture.height, TextureFormat.RGBA32, false );
            _drawingTexture.filterMode = FilterMode.Point;
            _pixelColors = new Color[ _renderTexture.width * _renderTexture.height ];
            ClearCanvas();

            InitBackgroundTexture();
        }
        public void DrawOnTexture( Vector2 normalizedPixelPosition, Color color )
        {
            //convert normalized position to RenderTexture position
            Vector2Int pixelPositionOnTexture = new( ( int ) ( normalizedPixelPosition.x * ( _renderTexture.width ) ),
                ( int ) ( normalizedPixelPosition.y * ( _renderTexture.height ) ) );

            // Calculate 1D array index (row-major order)
            int pixelIndex = pixelPositionOnTexture.y * _renderTexture.width + pixelPositionOnTexture.x;

            if ( pixelIndex < 0 || pixelIndex >= _pixelColors.Length )
                return;
            //If the color is the same don't call update(which is expensive operation)
            if ( _pixelColors[ pixelIndex ] == color )
                return;

            //Update color array and the texture
            _pixelColors[ pixelIndex ] = color;
            UpdateRenderTexture();
        }

        public void ClearCanvas()
        {
            for ( int i = 0; i < _pixelColors.Length; i++ )
            {
                _pixelColors[ i ] = Color.clear;
            }
            UpdateRenderTexture();
        }
        private void InitBackgroundTexture()
        {
            for ( int y = 0; y < _bgRenderTexture.height; y++ )
            {
                for ( int x = 0; x < _bgRenderTexture.width; x++ )
                {
                    bool isGrey = ( ( x / BG_BLOCK_SIZE ) % 2 == ( y / BG_BLOCK_SIZE ) % 2 );
                    _bgColors[ y * _bgRenderTexture.width + x ] = isGrey ? bgBlockColor1 : bgBlockColor2;
                }
            }

            _bgTexture.SetPixels( _bgColors );
            _bgTexture.Apply();

            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = _bgRenderTexture;
            Graphics.Blit( _bgTexture, _bgRenderTexture );
            RenderTexture.active = currentActiveRT;
        }
        private void UpdateRenderTexture()
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
