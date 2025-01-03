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
        EyeDropper,
        RectSelection,
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
        public int SizeLevel = 0;
        protected CSPaintingRenderer _csMainRenderer;
        protected CSPaintingRenderer _csPreviewRenderer;
        protected CSPaintingRenderer _csHoverRenderer;
        protected Vector2 _currentNormalizedPosition;
        protected Vector2Int _currentIndex;
        protected Color _currentColor;
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
            _currentColor = color;
            _currentNormalizedPosition = normalizedPixelPosition;
            var index = CSUtils.GetPixelIndex( normalizedPixelPosition, renderer.RT);
            DrawAtPixel( eCanvasType.Hover, index, color );
        }
        public virtual void Reload()
        {
            DrawOnHover( _currentNormalizedPosition, _currentColor );
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

            _currentIndex = pixelPosition;
            int radius = SizeLevel;

            for ( int x = -radius; x <= radius; x++ )
            {
                for ( int y = -radius; y <= radius; y++ )
                {
                    int distance = Mathf.CeilToInt(Mathf.Sqrt( x * x + y * y ));
                    if ( distance > radius )
                        continue;
                    (int x, int y) indexPair = (pixelPosition.x + x, pixelPosition.y + y );
                    int index = indexPair.y * renderer.RT.width + indexPair.x;
                    if ( !IsValidIndex( indexPair.x, indexPair.y, renderer ) )
                        continue;
                    renderer.PixelColors[ index ] = color;
                }
            }
            renderer.UpdateRenderTexture();
        }
        protected void DrawRectangle( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            int xMin = Mathf.Min( start.x, end.x );
            int xMax = Mathf.Max( start.x, end.x );
            int yMin = Mathf.Min( start.y, end.y );
            int yMax = Mathf.Max( start.y, end.y );

            for ( int x = xMin; x <= xMax; x++ )
            {
                DrawAtPixel( canvasType, new Vector2Int( x, yMin ), color );
                DrawAtPixel( canvasType, new Vector2Int( x, yMax ), color );
            }

            for ( int y = yMin; y <= yMax; y++ )
            {
                DrawAtPixel( canvasType, new Vector2Int( xMin, y ), color );
                DrawAtPixel( canvasType, new Vector2Int( xMax, y ), color );
            }

            GetRenderer( canvasType ).UpdateRenderTexture();
        }
        protected void DrawSquare( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            int width = Mathf.Abs( end.x - start.x );
            int height = Mathf.Abs( end.y - start.y );
            int size = Mathf.Max( width, height );

            // Adjust the end position to form a square without moving the start position
            if ( end.x >= start.x && end.y >= start.y )
            {
                end = new Vector2Int( start.x + size, start.y + size );
            }
            else if ( end.x >= start.x && end.y < start.y )
            {
                end = new Vector2Int( start.x + size, start.y - size );
            }
            else if ( end.x < start.x && end.y >= start.y )
            {
                end = new Vector2Int( start.x - size, start.y + size );
            }
            else
            {
                end = new Vector2Int( start.x - size, start.y - size );
            }

            // Draw the square using the adjusted end position
            int xMin = Mathf.Min( start.x, end.x );
            int xMax = Mathf.Max( start.x, end.x );
            int yMin = Mathf.Min( start.y, end.y );
            int yMax = Mathf.Max( start.y, end.y );

            for ( int x = xMin; x <= xMax; x++ )
            {
                DrawAtPixel( canvasType, new Vector2Int( x, yMin ), color );
                DrawAtPixel( canvasType, new Vector2Int( x, yMax ), color );
            }

            for ( int y = yMin; y <= yMax; y++ )
            {
                DrawAtPixel( canvasType, new Vector2Int( xMin, y ), color );
                DrawAtPixel( canvasType, new Vector2Int( xMax, y ), color );
            }

            GetRenderer( canvasType ).UpdateRenderTexture();
        }
        protected void DrawEllipse( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            var renderer = GetRenderer( canvasType );
            int a = Mathf.Abs( end.x - start.x ) / 2;
            int b = Mathf.Abs( end.y - start.y ) / 2;
            int centerX = ( start.x + end.x ) / 2;
            int centerY = ( start.y + end.y ) / 2;

            for ( int x = -a; x <= a; x++ )
            {
                int y = Mathf.RoundToInt( b * Mathf.Sqrt( 1 - ( x * x ) / ( float ) ( a * a ) ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY - y ), color );
            }

            for ( int y = -b; y <= b; y++ )
            {
                int x = Mathf.RoundToInt( a * Mathf.Sqrt( 1 - ( y * y ) / ( float ) ( b * b ) ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX - x, centerY + y ), color );
            }
        }
        protected void DrawCircle( eCanvasType canvasType, Vector2Int start, Vector2Int end, Color color )
        {
            var renderer = GetRenderer( canvasType );
            int radius = Mathf.Min( Mathf.Abs( end.x - start.x ), Mathf.Abs( end.y - start.y ) ) / 2;
            int centerX = ( start.x + end.x ) / 2;
            int centerY = ( start.y + end.y ) / 2;

            for ( int x = -radius; x <= radius; x++ )
            {
                int y = Mathf.RoundToInt( Mathf.Sqrt( radius * radius - x * x ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY - y ), color );
            }

            for ( int y = -radius; y <= radius; y++ )
            {
                int x = Mathf.RoundToInt( Mathf.Sqrt( radius * radius - y * y ) );
                DrawAtPixel( canvasType, new Vector2Int( centerX + x, centerY + y ), color );
                DrawAtPixel( canvasType, new Vector2Int( centerX - x, centerY + y ), color );
            }
        }
        protected bool IsValidIndex(int x, int y, CSPaintingRenderer renderer)
        {
            return x >= 0 && x < renderer.DrawingTexture.width && y >= 0 && y < renderer.DrawingTexture.height;
        }
        public virtual void IncreaseSize()
        {
            SizeLevel++;
            SizeLevel = Mathf.Clamp( SizeLevel, 0, 10 );
            Reload();
        }
        public virtual void DecreaseSize()
        {
            SizeLevel--;
            SizeLevel = Mathf.Clamp( SizeLevel, 0, 10 );
            Reload();
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
