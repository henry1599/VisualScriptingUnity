using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSCircle : CSBrush
    {
        private Vector2Int _startPosition;
        private bool _isDrawing;

        public override eBrushType BrushType => eBrushType.Circle;

        public override void DrawPointerDown( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            var renderer = GetRenderer( canvasType );
            GetRenderer(eCanvasType.Hover)?.ClearCanvas();
            _startPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
            _isDrawing = true;
        }

        public override void DrawPointerMove( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            if ( _isDrawing )
            {
                DrawPreview( normalizedPixelPosition, color );
            }
        }
        public override void DrawOnHover(Vector2 normalizedPixelPosition, Color color)
        {
            if (_isDrawing)
            {
                return;
            }
            base.DrawOnHover(normalizedPixelPosition, color);
        }

        public override void DrawPointerUp( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            if ( _isDrawing )
            {
                CSPaintingRenderer.CopyTo( _csPreviewRenderer, _csMainRenderer );
                _csPreviewRenderer.ClearCanvas();
                _csHoverRenderer.ClearCanvas();
                _isDrawing = false;
            }
        }

        public override void DrawPreview( Vector2 normalizedPixelPosition, Color color )
        {
            var renderer = GetRenderer( eCanvasType.Preview );
            Vector2Int endPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
            _csPreviewRenderer.ClearCanvas();
            Draw( eCanvasType.Preview, _startPosition, endPosition, color );
        }
        private void Draw( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            if ( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ) )
            {
                DrawCircle( canvasType, start, end, color );
            }
            else
            {
                DrawElipse( canvasType, start, end, color );
            }
        }
        private void DrawElipse( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            var renderer = GetRenderer( canvasType );
            int a = Mathf.Abs( end.x - start.x ) / 2;
            int b = Mathf.Abs( end.y - start.y ) / 2;
            int centerX = ( start.x + end.x ) / 2;
            int centerY = ( start.y + end.y ) / 2;

            for ( int x = -a; x <= a; x++ )
            {
                int y = Mathf.RoundToInt( b * Mathf.Sqrt( 1 - ( x * x ) / ( float ) ( a * a ) ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY - y ), color );
            }

            for ( int y = -b; y <= b; y++ )
            {
                int x = Mathf.RoundToInt( a * Mathf.Sqrt( 1 - ( y * y ) / ( float ) ( b * b ) ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX - x, centerY + y ), color );
            }
        }
        private void DrawCircle( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            var renderer = GetRenderer( canvasType );
            int radius = Mathf.Min( Mathf.Abs( end.x - start.x ), Mathf.Abs( end.y - start.y ) ) / 2;
            int centerX = ( start.x + end.x ) / 2;
            int centerY = ( start.y + end.y ) / 2;

            for ( int x = -radius; x <= radius; x++ )
            {
                int y = Mathf.RoundToInt( Mathf.Sqrt( radius * radius - x * x ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY - y ), color );
            }

            for ( int y = -radius; y <= radius; y++ )
            {
                int x = Mathf.RoundToInt( Mathf.Sqrt( radius * radius - y * y ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX - x, centerY + y ), color );
            }
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
