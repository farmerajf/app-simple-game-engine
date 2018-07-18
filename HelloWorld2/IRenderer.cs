using System;
using SkiaSharp;

namespace HelloWorld2
{
    public interface IRenderer
    {
        void Render(SKCanvas canvas, object gameObject);
    }
}
