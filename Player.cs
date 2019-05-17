using System;

namespace csharp_sfml
{
    public class Player : IGameObj
    {
        GameObject obj;

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
            // joystick
            if (InputHandler.Instance.IsJsInitialised)
            {
                // left right
                if (InputHandler.Instance.GetXValue(0, 1) > 0 ||
                    InputHandler.Instance.GetXValue(0, 1) < 0)
                {
                    // speed * dir
                    var vel = 2.5f * InputHandler.Instance.GetXValue(0, 1);
                    obj.SetVelX(vel);
                }

                // up down
                if (InputHandler.Instance.GetYValue(0, 1) > 0 ||
                    InputHandler.Instance.GetYValue(0, 1) < 0)
                {
                    var vel = 2.5f * InputHandler.Instance.GetYValue(0, 1);
                    obj.SetVelY(vel);
                }

                // diagonal 'slowdown'
                if ((InputHandler.Instance.GetXValue(0, 1) > 0 && InputHandler.Instance.GetYValue(0, 1) < 0) ||
                    (InputHandler.Instance.GetXValue(0, 1) > 0 && InputHandler.Instance.GetYValue(0, 1) > 0) ||
                    (InputHandler.Instance.GetXValue(0, 1) < 0 && InputHandler.Instance.GetYValue(0, 1) < 0) ||
                    (InputHandler.Instance.GetXValue(0, 1) < 0 && InputHandler.Instance.GetYValue(0, 1) > 0))
                {
                    // slow down character on diagonal movement
                    var s = 0.707106f; // sqrt of 0.5
                    obj.Velocity *= s;
                }

                if(InputHandler.Instance.GetButtonState(0,(uint)Buttons.RB)){
                    if (obj.Angle > 360)
                    {
                        obj.Angle = 0;
                    }
                    obj.Angle += 1;
                }

                if(InputHandler.Instance.GetButtonState(0,(uint)Buttons.LB)){
                    if (obj.Angle < 0)
                    {
                        obj.Angle = 360;
                    }
                    obj.Angle -= 1;
                }

            }
            // mouse / keys
            else
            {
                var target = InputHandler.Instance.GetMousePosition();
                var dis = MathHelper.Normalize(target - obj.Center());

                if (MathHelper.LengthSqu(target - obj.Center()) > 10)
                {
                    obj.Velocity = dis * 5;
                }

                // rotation
                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Q))
                {
                    // sfml uses degrees
                    if (obj.Angle < 0)
                    {
                        obj.Angle = 360;
                    }
                    obj.Angle -= 1;
                }
                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.E))
                {
                    // sfml uses degrees
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
