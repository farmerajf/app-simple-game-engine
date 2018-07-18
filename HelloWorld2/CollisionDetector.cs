using System;
using System.Collections.Generic;
using UIKit;

namespace HelloWorld2
{
    public class CollisionDetector
    {
        public enum CollisionLocation {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public void Detect(IList<GameObject> gameObjects)
        {
            for (var i = 0; i < gameObjects.Count; i++)
            {
                for (var j = i + 1; j < gameObjects.Count; j++)
                {
                    if (gameObjects[i] as Ball != null && gameObjects[j] as Ball != null)
                    {
                        var ball1 = gameObjects[i] as Ball;
                        var ball2 = gameObjects[j] as Ball;
                        var distanceBetween = Math.Sqrt(Math.Pow(ball1.X - ball2.X, 2) + Math.Pow(ball1.Y - ball2.Y, 2));
                        var totalRadius = ball1.Radius + ball2.Radius;

                        if (distanceBetween < totalRadius)
                        {
                            // Find a and h.
                            double a = (ball1.Radius * ball1.Radius -
                                        ball2.Radius * ball2.Radius + distanceBetween * distanceBetween) / (2 * distanceBetween);
                            double h = Math.Sqrt(ball1.Radius * ball1.Radius - a * a);

                            // Find P2.
                            double cx2 = ball1.X + a * (ball2.X - ball1.X) / distanceBetween;
                            double cy2 = ball1.Y + a * (ball2.Y - ball1.Y) / distanceBetween;

                            // Get the points P3.
                            var intersectionX = (cx2 + h * (ball2.Y - ball1.Y) / distanceBetween);
                            var intersectionY = (cy2 - h * (ball2.X - ball1.X) / distanceBetween);
                            /*intersection2 = new PointF(
                                (float)(cx2 - h * (cy1 - cy0) / dist),
                                (float)(cy2 + h * (cx1 - cx0) / dist));*/

                            ball2.Collision(ball1, GetCollisionLocation(ball2, intersectionX, intersectionY));
                            ball1.Collision(ball2, GetCollisionLocation(ball1, intersectionX, intersectionY));

                            var impact = new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Light);
                            impact.Prepare();
                            impact.ImpactOccurred();
                        }
                    }
                }
            }
        }

        private CollisionLocation GetCollisionLocation(Ball ball, double intersectionX, double intersectionY) {
            if (ball.X < intersectionX)
            {
                if (ball.Y < intersectionY)
                {
                    return CollisionLocation.BottomRight;
                }
                else
                {
                    return CollisionLocation.TopRight;
                }

            }
            else
            {
                if (ball.Y < intersectionY)
                {
                    return CollisionLocation.BottomLeft;
                }
                else
                {
                    return CollisionLocation.TopLeft;
                }
            }
        }
    }
}
