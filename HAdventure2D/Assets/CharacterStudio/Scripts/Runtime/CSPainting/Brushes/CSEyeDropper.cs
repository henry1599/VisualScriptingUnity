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

        public override void Setup(CSPaintingRenderer mainRenderer, CSPaintingRenderer previewRenderer, CSPaintingRenderer hoverRenderer)
        {
            base.Setup(mainRenderer, previewRenderer, hoverRenderer);
            SetSelfCursor();
        }
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
        }
        public override void IncreaseSize()
        {
        }
        public override void DecreaseSize()
        {
        }
        public override void HandleCursor(bool isEnter)
        {
            if (isEnter)
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
            Texture2D icon = CSPaintingManager.Instance.Setting.GetBrushCursor(BrushType);
            Vector2 hotpot = new Vector2(0, icon.height);
            Cursor.SetCursor(icon, hotpot, CursorMode.Auto);
            Cursor.visible = true;
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
