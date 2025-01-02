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
        }

        public override void DrawPreview(Vector2 normalizedPixelPosition, Color color)
        {
        }
        private void Fill(CSPaintingRenderer renderer, Vector2Int index, Color color)
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
