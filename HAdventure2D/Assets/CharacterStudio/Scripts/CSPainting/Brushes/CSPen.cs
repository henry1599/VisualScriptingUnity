using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPen : CSBrush
    {
        private Vector2Int? _previousPixelPosition = null;

        public override eBrushType BrushType => eBrushType.Pen;
        public override void Setup(CSPaintingRenderer mainRenderer, CSPaintingRenderer previewRenderer, CSPaintingRenderer hoverRenderer)
        {
            base.Setup(mainRenderer, previewRenderer, hoverRenderer);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        public override void DrawPointerDown(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            Vector2Int currentPixel = CSUtils.GetPixelIndex(normalizedPixelPosition, GetRenderer(canvasType).RT);
            DrawAtPixel(canvasType, currentPixel, color);
            _previousPixelPosition = currentPixel;
        }

        public override void DrawPointerMove(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            Vector2Int currentPixel = CSUtils.GetPixelIndex(normalizedPixelPosition, GetRenderer(canvasType).RT);

            if (_previousPixelPosition.HasValue)
            {
                Vector2Int previousPixel = _previousPixelPosition.Value;
                DrawSimpleLine(canvasType, previousPixel, currentPixel, color);
            }

            _previousPixelPosition = currentPixel;
        }

        public override void DrawPointerUp(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            _previousPixelPosition = null;
        }
        
        public override void DrawPreview(Vector2 normalizedPixelPosition, Color color)
        {
        }

        public override void HandleCursor(bool isEnter)
        {
            Cursor.visible = !isEnter;
        }
    }
}
