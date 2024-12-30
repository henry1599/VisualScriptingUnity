using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPen : CSBrush
    {
        public bool IsPixelPerfect = true;
        private Vector2Int? _previousPixelPosition = null;
        private HashSet<Vector2Int> _drawnPixels = new();

        public override eBrushType BrushType => eBrushType.Pen;

        public override void DrawPointerDown(Vector2 normalizedPixelPosition, Color color)
        {
            Vector2Int currentPixel = GetPixelPosition(normalizedPixelPosition);
            DrawAtPixel(currentPixel, color);
            _previousPixelPosition = currentPixel;
            _drawnPixels.Add(currentPixel);
        }

        public override void DrawPointerMove(Vector2 normalizedPixelPosition, Color color)
        {
            Vector2Int currentPixel = GetPixelPosition(normalizedPixelPosition);

            if (_previousPixelPosition.HasValue)
            {
                Vector2Int previousPixel = _previousPixelPosition.Value;
                DrawSimpleLine(previousPixel, currentPixel, color);
            }

            _previousPixelPosition = currentPixel;
            _drawnPixels.Add(currentPixel);
        }

        public override void DrawPointerUp(Vector2 normalizedPixelPosition, Color color)
        {
            _previousPixelPosition = null;
            _drawnPixels.Clear();
        }
        private void DrawSimpleLine(Vector2Int start, Vector2Int end, Color color)
        {
            List<Vector2Int> linePixels = GetLinePixels(start, end);

            foreach (var pixel in linePixels)
            {
                DrawAtPixel(pixel, color);
            }
        }

        private List<Vector2Int> GetLinePixels(Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> linePixels = new();
            int x0 = start.x;
            int y0 = start.y;
            int x1 = end.x;
            int y1 = end.y;

            int dx = Mathf.Abs(x1 - x0);
            int dy = Mathf.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                linePixels.Add(new Vector2Int(x0, y0));
                if (x0 == x1 && y0 == y1) break;

                int e2 = err * 2;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return linePixels;
        }
        private void DrawAtPixel(Vector2Int pixelPosition, Color color)
        {
            if (_csRenderer == null || _csRenderer.RT == null)
                return;

            int pixelIndex = pixelPosition.y * _csRenderer.RT.width + pixelPosition.x;

            if (pixelIndex < 0 || pixelIndex >= _csRenderer.PixelColors.Length)
                return;

            _csRenderer.PixelColors[pixelIndex] = color;
            _csRenderer.UpdateRenderTexture();
        }
        private Vector2Int GetPixelPosition(Vector2 normalizedPixelPosition)
        {
            int x = Mathf.Clamp((int)(normalizedPixelPosition.x * _csRenderer.RT.width), 0, _csRenderer.RT.width - 1);
            int y = Mathf.Clamp((int)(normalizedPixelPosition.y * _csRenderer.RT.height), 0, _csRenderer.RT.height - 1);
            return new Vector2Int(x, y);
        }
    }
}
