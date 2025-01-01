using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSEyeDropper : CSBrush
    {
        public override eBrushType BrushType => eBrushType.EyeDropper;
        Color? _previousColor = null;

        public override void DrawPointerDown(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            var mainRenderer = GetRenderer(eCanvasType.Main);
            var rt = mainRenderer.RT;
            Color capturedColor = mainRenderer.GetColorAtIndex(CSUtils.GetPixelIndex(normalizedPixelPosition, rt));
            if (_previousColor.HasValue && _previousColor.Value == capturedColor)
            {
                return;
            }
            EventBus.Instance.Publish(new OnColorPickedArgs(capturedColor));
        }

        public override void DrawPointerMove(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
            DrawPointerDown(canvasType, normalizedPixelPosition, color);
        }

        public override void DrawPointerUp(eCanvasType canvasType, Vector2 normalizedPixelPosition, Color color)
        {
        }

        public override void DrawPreview(Vector2 normalizedPixelPosition, Color color)
        {
        }
        public override void DrawOnHover(Vector2 normalizedPixelPosition, Color color)
        {
            base.DrawOnHover(normalizedPixelPosition, color);
        }

        public override void HandleCursor(bool isEnter)
        {
            if (!isEnter)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }
            Texture2D icon = CSPaintingManager.Instance.Setting.GetBrushCursor(eBrushType.EyeDropper);
            Vector2 hotpot = new Vector2(0, icon.height);
            Cursor.SetCursor(icon, hotpot, CursorMode.Auto);
        }

    }

    internal class OnColorPickedArgs : EventArgs
    {
        public Color CapturedColor;

        public OnColorPickedArgs(Color capturedColor)
        {
            CapturedColor = capturedColor;
        }
    }

}
