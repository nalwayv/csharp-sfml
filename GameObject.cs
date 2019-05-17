using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace csharp_sfml
{
    enum CollisionType
    {
        Circle, 
        Box,
    }

    public class GameObject : IGameObj
    {
        int objtype;
        bool isActive;
        string textureID;
        float radius;
        int width;
        int height;
        int callbackid;
        Node2D node2d;
        bool isanimated;
        int currentFrame;
        int currentRow;
        IntRect destRect;
        Animator animator;
        Collision collision;
        int collisionType;

        public bool IsActive { get => isActive; set => isActive = value; }
        public Vector2f Position { get => node2d.Position; set => node2d.Position = value; }
        public Vector2f Velocity { get => node2d.Velocity; set => node2d.Velocity = value; }
        public Vector2f Acceleration { get => node2d.Acceleration; set => node2d.Acceleration = value; }
        public float Scale { get => node2d.Scale; set => node2d.Scale = value; }
        public float Angle { get => node2d.Angle; set => node2d.Angle = value; } 
        public float Radius { get => radius; set => radius = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float Left { get => node2d.Position.X - width / 2; }
        public float Top { get => node2d.Position.Y - height / 2; }
        public float Right { get => node2d.Position.X + width / 2; }
        public float Bottom { get => node2d.Position.Y + height / 2; }
        public float CenterX { get => node2d.Position.X; }
        public float CenterY { get => node2d.Position.Y; }

        // mainly used for buttons
        public int CurrentFrame { get => currentFrame; set => currentFrame = value; }
        public int CurrentRow { get => currentRow; set => currentRow = value; }
        public int CallbackID { get => callbackid; set => callbackid = value; }

        public bool IsAnimated { get => isanimated; set => isanimated = value; }
        public Animator Animator { get => animator; set => animator = value; }
        public Collision Collision { get => collision; set => collision = value; }

        public GameObject()
        {
        }

        public Vector2f Center()
        {
            return new Vector2f(CenterX, CenterY);
        }

        public void SetVelX(float val)
        {
            node2d.SetVelocityX(val);
        }

        public void SetVelY(float val)
        {
            node2d.SetVelocityY(val);
        }

        public void Load(LoadParams p)
        {
            this.isActive = p.IsActive;

            this.objtype = p.Objtype;

            node2d = new Node2D(
                new Vector2f(p.X, p.Y),
                new Vector2f(),
                new Vector2f(),
                0.0f,
                1f);


            this.textureID = p.TextureID;

            this.width = p.Width;
            this.height = p.Height;
            this.radius = 45.0f;

            this.callbackid = p.CallBackID;


            collision = new Collision();
            collisionType = (int)CollisionType.Box;

            this.destRect = new IntRect();

            this.IsAnimated = p.IsAnimated;
            this.currentRow = 0;
            this.currentFrame = 0;
            if (IsAnimated)
            {
                animator = new Animator(p.Animations);
            }
        }

        public bool CollidesWith(GameObject other){
            if(!isActive || !other.isActive)
            {
                return false;
            }

            if(collisionType == (int)CollisionType.Box && other.collisionType == (int)CollisionType.Box)
            {
                return Collision.CollideBox(this, other);
            }

            if(collisionType == (int)CollisionType.Circle && other.collisionType == (int)CollisionType.Circle)
            {
                return Collision.CollideCircle(this, other);
            }
            return false;
        }

        public void Update()
        {
            if (!isActive)
            {
                return;
            }

            node2d.Velocity += node2d.Acceleration;
            node2d.Position += node2d.Velocity;

            if (IsAnimated)
            {
                destRect = animator.UpdateAnim();
            }
        }

        public void Draw()
        {
            if (!isActive)
            {
                return;
            }

            if (IsAnimated)
            {
                TextureHandler.Instance.DrawAnimation(
                        textureID,
                        (int)node2d.Position.X,
                        (int)node2d.Position.Y,
                        destRect.Left,
                        destRect.Top,
                        destRect.Width,
                        destRect.Height,
                        node2d.Angle,
                        Game.Instance.Window.GetWindow
                    );
            }
            else
            {
                TextureHandler.Instance.DrawFrame(
                    textureID,
                    (int)node2d.Position.X,
                    (int)node2d.Position.Y,
                    width,
                    height,
                    currentRow,
                    currentFrame,
                    Game.Instance.Window.GetWindow);
            }
        }

        public void Clean()
        {
            if (IsAnimated)
            {
                animator.Clear();
            }
        }
    }
}
