using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using SkiaSharp;
using System;
using System.Linq;
using UIKit;

namespace HelloWorld2
{
    public partial class HelloWorld2Page : ContentPage
    {
        private List<GameObject> items = new List<GameObject>();
        private CollisionDetector collisionDetector;

        public HelloWorld2Page()
        {
            collisionDetector = new CollisionDetector();
            InitializeComponent();
        }

        private Text debugText;

        protected override void OnAppearing()
        {
            debugText = new Text(100, 100, 30, "", "000000");
            items.Add(debugText);

            items.Add(new Text(110, 1100, 190, "Stupid Balls", "f4f5f7"));



            #pragma warning disable CS4014 
            GameLoop();
            #pragma warning restore CS4014
        }

        private async Task GameLoop() {
            while(true) {
                collisionDetector.Detect(items);
                canvasView.InvalidateSurface();
                if (ballInCreation != null) {
                    ContinueCreatingBall();
                }
                var fps = 1;
                if (items.Any(X => X.Speed > 0))
                {
                    fps = items.Where(X => X.Speed > 0).OrderBy(X => X.Speed).First().Speed;
                }
                await Task.Delay(fps);
            }
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();

            foreach (var item in items) {
                var renderer = RendererFactory.Renderer(item);
                renderer.Render(canvas, item);
            }

        }

        private Ball ballInCreation;
        private void ContinueCreatingBall() {
            ballInCreation.Radius = ballInCreation.Radius + 6;
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Moved) 
            {
                canvasView.InvalidateSurface();
            }
            if (ballInCreation == null)
            {
                e.Handled = true;
                foreach (var gameObject in items.AsEnumerable().Reverse())
                {
                    var ball = gameObject as Ball;
                    if (ball != null)
                    {
                        var distanceBetween = Math.Sqrt(Math.Pow(ball.X - e.Location.X, 2) + Math.Pow(ball.Y - e.Location.Y, 2));
                        if (distanceBetween <= ball.Radius)
                        {
                            items.Remove(ball);

                            var impact = new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Heavy);
                            impact.Prepare();
                            impact.ImpactOccurred();

                            return;
                        }
                    }

                }
            }

            if (e.ActionType == SKTouchAction.Pressed)
            {
                e.Handled = true;
                if (ballInCreation == null)
                {
                    var rnd = new Random();
                    ballInCreation = new Ball((int)e.Location.X,
                                              (int)e.Location.Y,
                                              20,
                                              rnd.Next(100),
                                              20 * 3,
                                              (int)canvasView.Width * 3,
                                              (int)canvasView.Height * 3,
                                              0,
                                              String.Format("#{0:X6}", rnd.Next(0x1000000)));
                    items.Add(ballInCreation);
                }
            }

            if (e.ActionType == SKTouchAction.Released && ballInCreation != null) {
                e.Handled = true;
                ballInCreation.Speed = GenerateSpeed(ballInCreation.Radius);
                ballInCreation = null;
            }

        }

        private int GenerateSpeed(int radius) {
            var maxSpeed = 10.0;
            var minSpeed = 5.0;
            var largeSize = 500.0;

            var speedIncrement = ((maxSpeed - minSpeed) / largeSize);
            var speed = radius * speedIncrement;

            if (speed > maxSpeed) {
                speed = maxSpeed;
            }
            if (speed < minSpeed) {
                speed = minSpeed;
            }

            //speed = (maxSpeed + 1) - speed;

            return (int)speed;
        }
    }
}
