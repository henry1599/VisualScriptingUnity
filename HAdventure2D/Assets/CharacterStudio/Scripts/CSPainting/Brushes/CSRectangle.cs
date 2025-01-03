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
            Vector2Int endPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
            _csPreviewRenderer.ClearCanvas();
            Draw( eCanvasType.Preview, _startPosition, endPosition, color );
        }
        private void Draw( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            if ( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ) )
            {
                DrawSquare( canvasType, start, end, color );
            }
            else
            {
                DrawRectangle( canvasType, start, end, color );
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
