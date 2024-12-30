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
        Fill
    }
    public abstract class CSBrush : MonoBehaviour
    {
        public abstract eBrushType BrushType { get; }
        protected CSPaintingRenderer _csRenderer;
        public void Initialize( CSPaintingRenderer renderer )
        {
            _csRenderer = renderer;
        }
        public abstract void DrawPointerDown( Vector2 normalizedPixelPosition, Color color );
        public abstract void DrawPointerMove( Vector2 normalizedPixelPosition, Color color );
        public abstract void DrawPointerUp( Vector2 normalizedPixelPosition, Color color );
    }
}
