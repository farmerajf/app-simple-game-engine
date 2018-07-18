using System;
using System.Collections.Generic;

namespace HelloWorld2
{
    public static class RendererFactory
    {
        private static Dictionary<Type, IRenderer> mapping = new Dictionary<Type, IRenderer> {
            {typeof(Ball), new BallRenderer()},
            {typeof(Text), new TextRenderer()}
        };

        public static IRenderer Renderer(object gameObject) {
            return mapping[gameObject.GetType()];
        }
    }
}
