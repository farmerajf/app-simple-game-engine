using System;
using SkiaSharp;

namespace HelloWorld2
{
    public class TextRenderer : IRenderer
    {
        public void Render(SKCanvas canvas, object gameObject)
        {
            var text = (Text)gameObject;
            var point = new SKPoint(text.X, text.Y);
            var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(text.Colour),
                StrokeWidth = 5,
                TextSize = text.Size
            };

            canvas.DrawText(text.Value, point, paint);
        }
    }
}
