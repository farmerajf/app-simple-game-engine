using SkiaSharp;

namespace HelloWorld2
{
    public class BallRenderer : IRenderer
    {
        public void Render(SKCanvas canvas, object gameObject) {
            var ball = (Ball)gameObject;
            var circleBorder = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(ball.Colour),
                StrokeWidth = 5
            };
            canvas.DrawCircle(ball.X, ball.Y, ball.Radius, circleBorder);
        }
    }
}
