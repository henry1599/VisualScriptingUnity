using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public enum eBrushType
    {
        Pen,
        Eraser,
        Line,
        Rectangle,
        Circle,
        Fill,
        EyeDropper
    }
    public enum eCanvasType
    {
        Main,
        Preview,
        Hover
    }
    public abstract class CSBrush : MonoBehaviour
    {
        public abstract eBrushType BrushType { get; }
        public TooltipData Tooltip;
        public ShortcutData Shortcut;
        public int SizeLevel = 1;
        protected CSPaintingRenderer _csMainRenderer;
        protected CSPaintingRenderer _csPreviewRenderer;
        protected CSPaintingRenderer _csHoverRenderer;
        public virtual void Setup( CSPaintingRenderer mainRenderer, CSPaintingRenderer previewRenderer, CSPaintingRenderer hoverRenderer )
        {
            _csMainRenderer = mainRenderer;
            _csPreviewRenderer = previewRenderer;
            _csHoverRenderer = hoverRenderer;
        }
        public virtual void Unsetup()
        {
            _csPreviewRenderer?.ClearCanvas();
            _csHoverRenderer?.ClearCanvas();
        }
        public virtual void SetDefaultCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        public abstract void SetSelfCursor();
        public abstract void DrawPointerDown( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color );
        public abstract void DrawPointerMove( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color );
        public abstract void DrawPointerUp( eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color );
        public abstract void HandleCursor( bool isEnter ); 
        // * Call when dragging a line, shapes, etc. and draw on a preview texture
        public abstract void DrawPreview( Vector2 normalizedPixelPosition, Color color );
        // * Call when hovering over the canvas, draw on a hover texture
        public virtual void DrawOnHover( Vector2 normalizedPixelPosition, Color color )
        {
            CSPaintingRenderer renderer = GetRenderer( eCanvasType.Hover );
            for ( int i = 0; i < renderer.PixelColors.Length; i++ )
            {
                renderer.PixelColors[ i ] = Color.clear;
            }
            Vector2Int index = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT);
            DrawAtPixel( eCanvasType.Hover, index, color );
        }
        protected void DrawSimpleLine( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color)
        {
            List<Vector2Int> linePixels = GetLinePixels(start, end);

            foreach (var pixel in linePixels)
            {
                DrawAtPixel(canvasType, pixel, color);
            }
        }
        protected List<Vector2Int> GetLinePixels(Vector2Int start, Vector2Int end)
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
        protected void DrawAtPixel(eCanvasType canvasType, Vector2Int pixelPosition, Color color)
        {
            CSPaintingRenderer renderer = GetRenderer(canvasType);
            if (renderer == null || renderer.RT == null)
                return;

            List<(int x, int y)> pixelIndexes = new ();
            Queue<(int x, int y)> pixelQueue = new();
            pixelQueue.Enqueue((pixelPosition.x, pixelPosition.y));
            int size = SizeLevel;
            while (pixelQueue.Count > 0)
            {
                var candidate = pixelQueue.Dequeue();
                pixelIndexes.Add(candidate);

                if (size < 0)
                    continue;
                List<(int x, int y)> neighbors = GetNeighborIndexes(candidate.x, candidate.y, renderer);
                foreach (var neighbor in neighbors)
                {
                    pixelQueue.Enqueue(neighbor);
                } 
                --size;
            }

            foreach (var pixelIndex in pixelIndexes)
            {
                if (!IsValidIndex(pixelIndex.x, pixelIndex.y, renderer))
                    return;
                int index = pixelIndex.y * renderer.RT.width + pixelIndex.x;
                renderer.PixelColors[index] = color;
                renderer.UpdateRenderTexture();
            }
        }
        protected List<(int x, int y)> GetNeighborIndexes(int x, int y, CSPaintingRenderer renderer)
        {
            int current = y * renderer.RT.width + x;
            int top = (y + 1) * renderer.RT.width + x;
            int bot = (y - 1) * renderer.RT.width + x;
            int left = y * renderer.RT.width + (x - 1);
            int right = y * renderer.RT.width + (x + 1);
            List<(int x, int y)> result = new();
            if (IsValidIndex(current, renderer))
                result.Add((x, y));
            if (IsValidIndex(top, renderer))
                result.Add((x, y + 1));
            if (IsValidIndex(bot, renderer))
                result.Add((x, y - 1));
            if (IsValidIndex(left, renderer))
                result.Add((x - 1, y));
            if (IsValidIndex(right, renderer))
                result.Add((x + 1, y));

            return result;
        }
        protected bool IsValidIndex(int x, int y, CSPaintingRenderer renderer)
        {
            int index = y * renderer.RT.width + x;
            return IsValidIndex(index, renderer);
        }
        protected bool IsValidIndex(int index, CSPaintingRenderer renderer)
        {
            return index >= 0 && index < renderer.PixelColors.Length;
        }
        protected CSPaintingRenderer GetRenderer(eCanvasType canvasType) => canvasType switch
        {
            eCanvasType.Main => _csMainRenderer,
            eCanvasType.Preview => _csPreviewRenderer,
            eCanvasType.Hover => _csHoverRenderer,
            _ => null
        };
    }
}
