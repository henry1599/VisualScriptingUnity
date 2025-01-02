using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSRectangle : CSBrush
    {
        private Vector2Int _startPosition;
        private bool _isDrawing;

        public override eBrushType BrushType => eBrushType.Rectangle;

        public override void DrawPointerDown( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            var renderer = GetRenderer( canvasType );
            _startPosition = new Vector2Int(
                Mathf.RoundToInt( normalizedPixelPosition.x * renderer.DrawingTexture.width ),
                Mathf.RoundToInt( normalizedPixelPosition.y * renderer.DrawingTexture.height )
            );
            _isDrawing = true;
        }

        public override void DrawPointerMove( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            if ( _isDrawing )
            {
                DrawPreview( normalizedPixelPosition, color );
            }
        }

        public override void DrawPointerUp( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            if ( _isDrawing )
            {
                CSPaintingRenderer.CopyTo(_csPreviewRenderer, _csMainRenderer );
                _csPreviewRenderer.ClearCanvas();
                _csHoverRenderer.ClearCanvas();
                _isDrawing = false;
            }
        }

        public override void DrawPreview( Vector2 normalizedPixelPosition, Color color )
        {
            var renderer = GetRenderer( eCanvasType.Preview );
            Vector2Int endPosition = new Vector2Int(
                Mathf.RoundToInt( normalizedPixelPosition.x * renderer.DrawingTexture.width ),
                Mathf.RoundToInt( normalizedPixelPosition.y * renderer.DrawingTexture.height )
            );
            _csPreviewRenderer.ClearCanvas();
            DrawRectangle( eCanvasType.Preview, _startPosition, endPosition, color );
        }

        private void DrawRectangle( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            int xMin = Mathf.Min( start.x, end.x );
            int xMax = Mathf.Max( start.x, end.x );
            int yMin = Mathf.Min( start.y, end.y );
            int yMax = Mathf.Max( start.y, end.y );

            for ( int x = xMin; x <= xMax; x++ )
            {
                DrawAtPixel( canvasType, new Vector2Int( x, yMin ), color );
                DrawAtPixel( canvasType, new Vector2Int( x, yMax ), color );
            }

            for ( int y = yMin; y <= yMax; y++ )
            {
                DrawAtPixel( canvasType, new Vector2Int( xMin, y ), color );
                DrawAtPixel( canvasType, new Vector2Int( xMax, y ), color );
            }

            GetRenderer( canvasType ).UpdateRenderTexture();
        }

        public override void IncreaseSize()
        {
        }

        public override void DecreaseSize()
        {
        }

        public override void HandleCursor( bool isEnter )
        {
            if ( isEnter )
            {
                SetSelfCursor();
            }
            else
            {
                SetDefaultCursor();
            }
        }

        public override void SetSelfCursor()
        {
            Texture2D icon = CSPaintingManager.Instance.Setting.GetBrushCursor( BrushType );
            Vector2 hotpot = new Vector2( icon.width / 2f, icon.height / 2f );
            Cursor.SetCursor( icon, hotpot, CursorMode.Auto );
            Cursor.visible = true;
        }
    }
}
