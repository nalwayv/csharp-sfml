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

    public class GameData
    {
        public int ObjType;
        public bool IsActive;
        public string TextureID;
        public float Radius;
        public int Width;
        public int Height;
        public int CallBackID;
        public bool IsAnimated;
        public int CurrentFrame;
        public int CurrentRow;
        public float Angle;
        public int CollisionType;
        public float Scale;

        public float Top;
        public float Bottom;
        public float Left;
        public float Right;

        public IntRect TextureDestination;
        public Vector2f Position;
        public Vector2f Velocity;
        public Vector2f Acceleration;
        public Vector2f Center;
        public Animator Animator;
        public Collision Collisions;

        public GameData()
        {
            ObjType = 0;
            IsActive = false;
            TextureID = "";
            Radius = 0.0f;
            Width = 0;
            Height = 0;
            CallBackID = 0;
            IsAnimated = false;
            CurrentFrame = 0;
            CurrentRow = 0;
            CollisionType = 0;
            Angle = 0.0f;
            Scale = 0.0f;
            Top = 0;
            Bottom = 0;
            Left = 0;
            Right = 0;
            TextureDestination = new IntRect(0, 0, 0, 0);
            Position = new Vector2f(0, 0);
            Velocity = new Vector2f(0, 0);
            Acceleration = new Vector2f(0, 0);
            Center = new Vector2f();
            Animator = null;
            Collisions = null;
        }
    }

    public class GameObject : IGameObj
    {
        GameData data;

        public bool IsActive { get => data.IsActive; set => data.IsActive = value; }
        public Vector2f Position { get => data.Position; set => data.Position = value; }
        public Vector2f Velocity { get => data.Velocity; set => data.Velocity = value; }
        public Vector2f Acceleration { get => data.Acceleration; set => data.Acceleration = value; }
        public float Angle { get => data.Angle; set => data.Angle = value; }
        public float Radius { get => data.Radius; set => data.Radius = value; }
        public int Width { get => data.Width; set => data.Width = value; }
        public int Height { get => data.Height; set => data.Height = value; }
        public int CurrentFrame { get => data.CurrentFrame; set => data.CurrentFrame = value; }
        public Animator Animator { get => data.Animator; set => data.Animator = value; }
        public GameData Data { get => data; }

        public GameObject()
        {
        }

        public Vector2f Center()
        {
            return new Vector2f(data.Position.X, data.Position.Y);
        }

        public void SetVelX(float val)
        {
            data.Velocity.X = val;
        }

        public void SetVelY(float val)
        {
            data.Velocity.Y = val;
        }

        public void Load(LoadParams p)
        {
            data = new GameData();
            data.IsActive = p.IsActive;
            data.ObjType = p.Objtype;
            data.Position.X = p.X;
            data.Position.Y = p.Y;
            data.Scale = 1.0f;
            data.TextureID = p.TextureID;
            data.Width = p.Width;
            data.Height = p.Height;
            data.Left = data.Position.X - (data.Width * 0.5f);
            data.Top = data.Position.Y - (data.Height * 0.5f);
            data.Right = data.Position.X + (data.Width * 0.5f);
            data.Bottom = data.Position.Y + (data.Height * 0.5f);
            data.CallBackID = p.CallBackID;
            data.CollisionType = (int)CollisionType.Box;
            data.IsAnimated = p.IsAnimated;
            data.Center.X = data.Position.X;
            data.Center.Y = data.Position.Y;
            if (data.IsAnimated)
            {
                data.Animator = new Animator(p.Animations);
            }
            data.Collisions = new Collision();
        }

        public bool CollidesWith(GameData other)
        {
            if (!data.IsActive || !other.IsActive)
            {
                return false;
            }

            if (data.CollisionType == (int)CollisionType.Box && other.CollisionType == (int)CollisionType.Box)
            {
                return data.Collisions.CollideBox(data, other);
            }

            if (data.CollisionType == (int)CollisionType.Circle && other.CollisionType == (int)CollisionType.Circle)
            {
                return data.Collisions.CollideCircle(data, other);
            }
            return false;
        }

        public void Update()
        {
            if (!data.IsActive)
            {
                return;
            }

            data.Velocity += data.Acceleration;
            data.Position += data.Velocity;

            if (data.IsAnimated)
            {
                data.TextureDestination = data.Animator.UpdateAnim();
            }
        }

        public void Draw()
        {
            if (!data.IsActive)
            {
                return;
            }

            if (data.IsAnimated)
            {
                TextureHandler.Instance.DrawAnimation(
                        data.TextureID,
                        (int)data.Position.X,
                        (int)data.Position.Y,
                        data.TextureDestination.Left,
                        data.TextureDestination.Top,
                        data.TextureDestination.Width,
                        data.TextureDestination.Height,
                        data.Angle,
                        Game.Instance.Window.GetWindow
                    );
            }
            else
            {
                TextureHandler.Instance.DrawFrame(
                    data.TextureID,
                    (int)data.Position.X,
                    (int)data.Position.Y,
                    data.Width,
                    data.Height,
                    data.CurrentRow,
                    data.CurrentFrame,
                    Game.Instance.Window.GetWindow);
            }
        }

        public void Clean()
        {
            if (data.IsAnimated)
            {
                data.Animator.Clear();
            }
        }
    }
}
