using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPaintingBackgroundRenderer : CSPaintingRenderer
    {
        int _backgroundBlockSize = 16;
        Color _bgBlockColor1 = new Color( 0.8f, 0.8f, 0.8f, 1f );
        Color _bgBlockColor2 = new Color( 0.6f, 0.6f, 0.6f, 1f );
        public void Setup(CSPaintingSetting setting)
        {
            StartCoroutine( Cor_Setup(setting) );
        }
        IEnumerator Cor_Setup(CSPaintingSetting setting)
        {
            yield return new WaitUntil(() => _isSetup);
            _backgroundBlockSize = setting.BackgroundTileSize;
            _bgBlockColor1 = setting.BackgroundTileColor1;
            _bgBlockColor2 = setting.BackgroundTileColor2;
            InitBackgroundTexture();
        }
        private void InitBackgroundTexture()
        {
            _pixelColors = new Color[ _renderTexture.width * _renderTexture.height ];
            _drawingTexture = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };
            for ( int y = 0; y < _renderTexture.height; y++ )
            {
                for ( int x = 0; x < _renderTexture.width; x++ )
                {
                    bool isGrey =   x / _backgroundBlockSize  % 2 ==  y / _backgroundBlockSize  % 2 ;
                    _pixelColors[ y * _renderTexture.width + x ] = isGrey ? _bgBlockColor1 : _bgBlockColor2;
                }
            }

            _drawingTexture.SetPixels( _pixelColors );
            _drawingTexture.Apply();

            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = _renderTexture;
            Graphics.Blit( _drawingTexture, _renderTexture );
            RenderTexture.active = currentActiveRT;
        }
    }
}
