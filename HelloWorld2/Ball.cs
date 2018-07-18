using System.Timers;
using SkiaSharp;

namespace HelloWorld2
{
    public class Ball : GameObject
    {
        public int Radius { get; set; }

        public int XDirection { get; set; }
        public int YDirection { get; set; }

        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public string Colour { get; set; }

        private int _speed;
        public new int Speed
        {
            get
            {
                return _speed;
            }
            set {
                _speed = value;
                if (value > 0)
                {
                    timer.Interval = value;
                }
            }
        }

        private Timer timer;

        public Ball(int x, int y, int radius, int minX, int minY, int maxX, int maxY, int speed, string colour)
        {
            X = x;
            Y = y;
            Radius = radius;
            Width = radius * 2;
            Height = radius * 2;

            XDirection = 1;
            YDirection = 1;

            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;



            Colour = colour;

            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            Speed = speed;
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            X = X + XDirection;
            Y = Y + YDirection;

            if (X > MaxX - Radius)
            {
                XDirection = -1;
            }
            if (X < MinX + Radius)
            {
                XDirection = 1;
            }
            if (Y > MaxY - Radius)
            {
                YDirection = -1;
            }
            if (Y < MinY + Radius)
            {
                YDirection = 1;
            }
        }

        public override void Collision(GameObject gameObject, CollisionDetector.CollisionLocation collisionLocation) {
            if (gameObject as Ball != null)
            {

                //XDirection = XDirection * -1;
                //YDirection = YDirection * -1;

                 switch (collisionLocation) {
                    case CollisionDetector.CollisionLocation.TopLeft:
                        YDirection = 1;
                        XDirection = 1;
                        break;
                    case CollisionDetector.CollisionLocation.TopRight:
                        YDirection = 1;
                        XDirection = -1;
                        break;
                    case CollisionDetector.CollisionLocation.BottomLeft:
                        YDirection = -1;
                        XDirection = 1;
                        break;
                    case CollisionDetector.CollisionLocation.BottomRight:
                        YDirection = -1;
                        XDirection = -1;
                        break;
                } 
            }
        } 
    }
}
