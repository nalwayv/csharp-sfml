using System;
using SFML.System;

namespace csharp_sfml
{
    public class Node2D
    {
        Vector2f position;
        Vector2f velocity;
        Vector2f acceleration;
        float angle;
        float scale;

        public Vector2f Position { get => position; set => position = value; }
        public Vector2f Velocity { get => velocity; set => velocity = value; }
        public Vector2f Acceleration { get => acceleration; set => acceleration = value; }
        public float Angle { get => angle; set => angle = value; }
        public float Scale { get => scale; set => scale = value; }

        public Node2D(
            Vector2f position,
            Vector2f velocity,
            Vector2f acceleration,
            float angle,
            float scale)
        {
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.angle = angle;
            this.scale = scale;
        }

        public void SetPositionX(float value)
        {
            position.X = value;
        }

        public void SetPositionY(float value)
        {
            position.Y = value;
        }

        public void SetVelocityX(float value)
        {
            velocity.X = value;
        }

        public void SetVelocityY(float value)
        {
            velocity.Y = value;
        }
    }
}