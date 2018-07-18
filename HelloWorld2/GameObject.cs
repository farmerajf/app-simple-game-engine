using System;
namespace HelloWorld2
{
    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int Speed { get; set; }

        public virtual void Collision(GameObject gameObject, CollisionDetector.CollisionLocation collisionLocation) {
            
        }
    }
}
