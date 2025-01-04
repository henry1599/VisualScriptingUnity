using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSSelection : CSBrush
    {
        public Color selectColor;
        private Vector2Int? _startPosition = null;
        private Vector2Int? _endPosition = null;
        private bool _isDrawing = false;
        private bool _isDraggingSelection = false;
        private Vector2Int _dragOffset;
        private Color[] _selectionPixels;

        public override eBrushType BrushType => eBrushType.RectSelection;

        public override void DrawPointerDown( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            var renderer = GetRenderer( canvasType );
            var touchPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
            if ( _startPosition.HasValue && _endPosition.HasValue )
            {
                if ( IsInsideSelectionArea( touchPosition, _startPosition.Value, _endPosition.Value ) )
                {
                    var previewRenderer = GetRenderer( eCanvasType.Preview );
                    _isDraggingSelection = true;
                    _isDrawing = false;
                    _dragOffset = touchPosition - _startPosition.Value;
                    _selectionPixels = CSPaintingRenderer.SaveToArray(renderer, _startPosition.Value, _endPosition.Value);
                    renderer.ClearInRect( _startPosition.Value, _endPosition.Value );
                    CSPaintingRenderer.LoadArrayToRenderer(previewRenderer, _selectionPixels, _startPosition.Value, _endPosition.Value);
                }
                else
                {
                    GetRenderer(eCanvasType.Preview).ClearCanvas();
                    _startPosition = touchPosition;
                    _endPosition = null;
                    _isDrawing = true;
                    _isDraggingSelection = false;
                }
            }
            if ( !_endPosition.HasValue )
            {
                color = selectColor;
                _startPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
                _isDrawing = true;
                _isDraggingSelection = false;
            }
        }

        public override void DrawPointerMove( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            if ( _isDrawing && !_isDraggingSelection )
            {
                DrawPreview( normalizedPixelPosition, color );
            }
            else if ( !_isDrawing && _isDraggingSelection )
            {
                MoveSelection( normalizedPixelPosition, color );
            }
        }

        public override void DrawPointerUp( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color )
        {
            if ( _isDrawing )
            {
                _isDrawing = false;
            }
            else if ( _isDraggingSelection )
            {
                CSPaintingRenderer.CopyToRect( GetRenderer(eCanvasType.Preview), GetRenderer(eCanvasType.Main), _startPosition.Value, _endPosition.Value );
                _startPosition = _endPosition = null;
                _selectionPixels = null;
                _isDraggingSelection = false;
                RegisterState();
            }
        }

        public override void DrawPreview( Vector2 normalizedPixelPosition, Color color )
        {
            color = selectColor;
            var renderer = GetRenderer( eCanvasType.Preview );
            _endPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
            renderer.ClearCanvas();
            Draw( eCanvasType.Preview, _startPosition.Value, _endPosition.Value, color );
        }

        private void MoveSelection( Vector2 normalizedPixelPosition, Color color )
        {
            var renderer = GetRenderer( eCanvasType.Preview );
            var touchPosition = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT );
            var newStartPosition = touchPosition - _dragOffset;
            var newEndPosition = newStartPosition + ( _endPosition.Value - _startPosition.Value );

            renderer.ClearCanvas();
            CSPaintingRenderer.LoadArrayToRenderer(renderer, _selectionPixels, newStartPosition, newEndPosition);

            _startPosition = newStartPosition;
            _endPosition = newEndPosition;
        }

        private void Draw( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            DrawRectangle( canvasType, start, end, color );
        }

        public override void DrawOnHover( Vector2 normalizedPixelPosition, Color color )
        {
            color = selectColor;
            base.DrawOnHover( normalizedPixelPosition, color );
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

        private bool IsInsideSelectionArea( Vector2Int touch, Vector2Int start, Vector2Int end )
        {
            int minX = Mathf.Min( start.x, end.x );
            int maxX = Mathf.Max( start.x, end.x );
            int minY = Mathf.Min( start.y, end.y );
            int maxY = Mathf.Max( start.y, end.y );

            return touch.x >= minX && touch.x <= maxX && touch.y >= minY && touch.y <= maxY;
        }
    }
}
