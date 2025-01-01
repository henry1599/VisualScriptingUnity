using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSEraser : CSBrush
    {
        private Vector2Int? _previousPixelPosition = null;

        public override eBrushType BrushType => eBrushType.Eraser;
        public override void Setup(CSPaintingRenderer mainRenderer, CSPaintingRenderer previewRenderer, CSPaintingRenderer hoverRenderer)
        {
            base.Setup(mainRenderer, previewRenderer, hoverRenderer);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        public override void DrawPointerDown(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            color = Color.clear;
            Vector2Int currentPixel = CSUtils.GetPixelIndex(normalizedPixelPosition, GetRenderer(canvasType).RT);
            DrawAtPixel(canvasType, currentPixel, color);
            _previousPixelPosition = currentPixel;
        }

        public override void DrawPointerMove(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            color = Color.clear;
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
        public override void DrawOnHover(Vector2 normalizedPixelPosition, Color color)
        {
            color = new Color(0, 0, 0, 0.25f);
            base.DrawOnHover(normalizedPixelPosition, color);
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
