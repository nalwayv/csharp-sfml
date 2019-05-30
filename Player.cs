using System;

namespace csharp_sfml
{
    public class Player : IGameObj
    {
        GameObject obj;
        const float Speed = 2.5f;

        public GameObject Obj { get => obj; set => obj = value; }

        public Player()
        {
            this.obj = new GameObject();
        }

        public void Load(LoadParams p)
        {
            obj.Load(p);
            obj.Radius = 20;
        }

        public void Update()
        {
            obj.Velocity -= obj.Velocity;

            HandleInput();

            obj.Update();
        }

        public void Draw()
        {
            obj.Draw();
        }

        public void Clean()
        {
            obj.Clean();
        }

        // -- INPUT

        private void HandleInput()
        {
            if (InputHandler.Instance.IsJsInitialised)
            {
                // left right
                if (InputHandler.Instance.GetXValue(0, 1) > 0 ||
                    InputHandler.Instance.GetXValue(0, 1) < 0)
                {
                    obj.SetVelX(InputHandler.Instance.GetXValue(0, 1));
                }

                // up down
                if (InputHandler.Instance.GetYValue(0, 1) > 0 ||
                    InputHandler.Instance.GetYValue(0, 1) < 0)
                {
                    obj.SetVelY(InputHandler.Instance.GetYValue(0, 1));
                }

                // diagonal 'slowdown'
                if ((InputHandler.Instance.GetXValue(0, 1) > 0 && InputHandler.Instance.GetYValue(0, 1) < 0) ||
                    (InputHandler.Instance.GetXValue(0, 1) > 0 && InputHandler.Instance.GetYValue(0, 1) > 0) ||
                    (InputHandler.Instance.GetXValue(0, 1) < 0 && InputHandler.Instance.GetYValue(0, 1) < 0) ||
                    (InputHandler.Instance.GetXValue(0, 1) < 0 && InputHandler.Instance.GetYValue(0, 1) > 0))
                {
                    obj.Velocity *= 0.707106f;
                }

                if (InputHandler.Instance.GetButtonState(0, (uint)Buttons.RB))
                {
                    if (obj.Angle > 360)
                    {
                        obj.Angle = 0;
                    }
                    obj.Angle += 1;
                }

                if (InputHandler.Instance.GetButtonState(0, (uint)Buttons.LB))
                {
                    if (obj.Angle < 0)
                    {
                        obj.Angle = 360;
                    }
                    obj.Angle -= 1;
                }

                if (MathHelper.LengthSqu(obj.Velocity) > 0)
                {
                    obj.Velocity = MathHelper.Normalize(obj.Velocity) * Speed;
                    obj.Animator.SwitchAnimation("walk");

                }
                else
                {
                    obj.Animator.SwitchAnimation("stand");
                }
            }
            else
            {
                var target = InputHandler.Instance.GetMousePosition();
                var direction = target - obj.Center();
                var dirNormal = MathHelper.Normalize(direction);

                // follow mouse
                if (MathHelper.LengthSqu(direction) > 10)
                {
                    obj.Velocity = dirNormal * Speed;
                }

                // look at mouse
                var lookAt = MathHelper.LookAt2D(obj.Center(), target);
                var toAngle = MathHelper.Angle(lookAt);
                obj.Angle = MathHelper.ToDegrees(toAngle);


                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Q))
                {
                    if (obj.Angle < 0)
                    {
                        obj.Angle = 360;
                    }
                    obj.Angle -= 1;
                }
                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.E))
                {
                    if (obj.Angle > 360)
                    {
                        obj.Angle = 0;
                    }
                    obj.Angle += 1;
                }

                // gameover
                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.D))
                {
                    Game.Instance.StateMachine.ChangeState(new GameOverState());
                }

                // change animations
                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Down))
                {
                    obj.Animator.SwitchAnimation("duck");
                }

                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Left) ||
                    InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Right))
                {
                    obj.Animator.SwitchAnimation("walk");
                }

                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Up))
                {
                    obj.Animator.SwitchAnimation("stand");
                }
            }
        }
    }
}
