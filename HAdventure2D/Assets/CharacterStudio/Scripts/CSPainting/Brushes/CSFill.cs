using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSFill : CSBrush
    {
        public override eBrushType BrushType => eBrushType.Fill;

        public override void DrawPointerDown(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            var renderer = GetRenderer(canvasType);
            var indexPair = CSUtils.GetPixelIndex(normalizedPixelPosition, renderer.RT);
            var index = indexPair.y * renderer.RT.width + indexPair.x;
            Color pointColor = renderer.PixelColors[index];
            if (pointColor == color)
                return;
            Fill(renderer, indexPair, color);
        }

        public override void DrawPointerMove(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
        }

        public override void DrawPointerUp(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            RegisterState();
        }

        public override void DrawPreview(Vector2 normalizedPixelPosition, Color color)
        {
        }
        private void Fill(CSPaintingRenderer renderer, Vector2Int index, Color color)
        {
            List<Vector2Int> visited = new();
            Queue<Vector2Int> queue = new();
            queue.Enqueue(index);
            visited.Add(index);
            Color targetColor = renderer.PixelColors[index.y * renderer.RT.width + index.x];
            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                renderer.PixelColors[current.y * renderer.RT.width + current.x] = color;
                var neighbors = GetNeighbors(renderer, current);
                foreach (var neighbor in neighbors)
                {
                    if (visited.Contains(neighbor))
                        continue;
                    if (renderer.PixelColors[neighbor.y * renderer.RT.width + neighbor.x] == targetColor)
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
            renderer.UpdateRenderTexture();
        }
        List<Vector2Int> GetNeighbors(CSPaintingRenderer renderer, Vector2Int index)
        {
            List<Vector2Int> result = new();
            Vector2Int top = index + Vector2Int.up;
            Vector2Int bottom = index + Vector2Int.down;
            Vector2Int left = index + Vector2Int.left;
            Vector2Int right = index + Vector2Int.right;
            if (top.y < renderer.RT.height)
                result.Add(top);
            if (bottom.y >= 0)
                result.Add(bottom);
            if (left.x >= 0)
                result.Add(left);
            if (right.x < renderer.RT.width)
                result.Add(right);
            return result;
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
