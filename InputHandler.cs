using System;
using System.Collections.Generic;
using SFML.System;

namespace csharp_sfml
{
    #region BUTTONS
    /*
    *X_BOX BUTTONS*
    
    | Symbol        |  Num  |
    | :------------ | :---: |
    | A             |   0   |
    | B             |   1   |
    | X             |   2   |
    | Y             |   3   |
    | LB            |   4   |
    | RB            |   5   |
    | SELECT        |   6   |
    | START         |   7   |
    | L-STICK-PRESS |   8   |
    | R-STICK-PRESS |   9   |
    **/
    public enum Buttons
    {
        A = 0,
        B,
        X,
        Y,
        LB,
        RB,
        SELECT,
        START,
        L_STICK_PRESS,
        R_STICK_PRESS
    }

    /*
    | MOUSE-CLICK |  Num  |
    | :---------- | :---: |
    | LEFT        |   0   |
    | RIGHT       |   1   |
    | MIDDLE      |   2   |
    **/
    public enum MouseButons
    {
        LEFT = 0,
        RIGHT,
        MIDDLE
    }
    #endregion

    #region JOYSTICKS
    public class JStick
    {
        const float DEADZONE = 50.0f;

        int id;
        //sticks
        Vector2f first;
        Vector2f second;
        // buttons
        List<bool> inButtons;

        public Vector2f First { get => first; set => first = value; }
        public Vector2f Second { get => second; set => second = value; }
        public float FirstX { get => first.X; set => first.X = value; }
        public float FirstY { get => first.X; set => first.X = value; }
        public float SecondX { get => second.X; set => second.X = value; }
        public float SecondY { get => second.X; set => second.X = value; }

        public JStick(int id)
        {
            Init(id);
        }

        public void Init(int id)
        {
            // set id
            this.id = id;

            // sticks
            first = new Vector2f(0, 0);
            second = new Vector2f(0, 0);
            inButtons = new List<bool>();

            // buttons
            var numButtons = SFML.Window.Joystick.GetButtonCount((uint)id);
            for (var b = 0; b < numButtons; b++)
            {
                inButtons.Add(false);
            }
        }

        public void OnJoyButtonDown(uint button)
        {
            inButtons[(int)button] = true;
        }

        public void OnJoyButtonUp(uint button)
        {
            inButtons[(int)button] = false;
        }

        public bool GetButtonState(uint button)
        {
            return inButtons[(int)button];
        }


        public int GetXValue(int stick)
        {
            // left stick (x,y)
            if (stick == 1)
            {
                return (int)first.X;
            }
            // right stick (y,u)
            else if (stick == 2)
            {
                return (int)second.X;
            }

            return 0;
        }

        public int GetYValue(int stick)
        {

            // left stick (x,y)
            if (stick == 1)
            {
                return (int)first.Y;
            }
            // right stick (y,u)
            else if (stick == 2)
            {
                return (int)second.Y;
            }

            return 0;
        }

        public void OnJoyAxisMove(SFML.Window.Joystick.Axis axis)
        {
            // --- left stick
            if (axis == SFML.Window.Joystick.Axis.X)
            {
                // left
                if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) > DEADZONE)
                {
                    first.X = 1;
                }
                // right
                else if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) < -DEADZONE)
                {
                    first.X = -1;
                }
                // reset
                else
                {
                    first.X = 0;
                }
            }

            if (axis == SFML.Window.Joystick.Axis.Y)
            {
                // left
                if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) > DEADZONE)
                {
                    first.Y = 1;
                }
                // right
                else if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) < -DEADZONE)
                {
                    first.Y = -1;
                }
                // reset
                else
                {
                    first.Y = 0;
                }
            }

            // --- right stick
            if (axis == SFML.Window.Joystick.Axis.V)
            {
                // left
                if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) > DEADZONE)
                {
                    second.X = 1;
                }
                // right
                else if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) < -DEADZONE)
                {
                    second.X = -1;
                }
                // reset
                else
                {
                    second.X = 0;
                }
            }

            if (axis == SFML.Window.Joystick.Axis.U)
            {
                // left
                if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) > DEADZONE)
                {
                    second.Y = 1;
                }
                // right
                else if (SFML.Window.Joystick.GetAxisPosition((uint)id, axis) < -DEADZONE)
                {
                    second.Y = -1;
                }
                // reset
                else
                {
                    second.Y = 0;
                }
            }
        }
    }
    #endregion

    #region INPUTHANDLER
    public class InputHandler
    {
        bool jsInitialised;
        Dictionary<SFML.Window.Keyboard.Key, bool> keys;
        Vector2f mouePos;
        bool[] mouseButtons;
        Dictionary<int, JStick> jsticks;
        public bool IsJsInitialised { get => jsInitialised; set => jsInitialised = value; }

        // --- Singleton
        private static readonly Lazy<InputHandler> instance = new Lazy<InputHandler>(() => new InputHandler());
        public static InputHandler Instance { get => instance.Value; }
        private InputHandler()
        {
            // keyboard
            keys = new Dictionary<SFML.Window.Keyboard.Key, bool>();

            // mouse
            mouePos = new Vector2f(0, 0);
            mouseButtons = new bool[3];

            // sticks
            jsticks = new Dictionary<int, JStick>();
            jsInitialised = false;
        }


        // --- poll events
        public void Update()
        {
            Game.Instance.Window.GetWindow.Closed += (s, e) => Game.Instance.Window.GetWindow.Close();

            // --- joy
            Game.Instance.Window.GetWindow.JoystickButtonPressed += (s, e) =>
            {
                if (IsJsInitialised)
                {
                    var which = e.JoystickId;
                    var button = e.Button;
                    var d = jsticks[(int)which];
                    d.OnJoyButtonDown(button);
                }
            };

            Game.Instance.Window.GetWindow.JoystickButtonReleased += (s, e) =>
            {
                if (IsJsInitialised)
                {
                    var which = e.JoystickId;
                    var button = e.Button;
                    var d = jsticks[(int)which];
                    d.OnJoyButtonUp(button);
                }
            };

            Game.Instance.Window.GetWindow.JoystickMoved += (s, e) =>
            {
                SFML.Window.Joystick.Update();

                if (IsJsInitialised)
                {
                    var which = e.JoystickId;
                    var axis = e.Axis;

                    var d = jsticks[(int)which];
                    d.OnJoyAxisMove(axis);

                }
            };

            // --- mouse
            Game.Instance.Window.GetWindow.MouseMoved += (s, e) =>
            {
                OnMouseMove(e.X, e.Y);
            };

            Game.Instance.Window.GetWindow.MouseButtonPressed += (s, e) =>
            {
                var button = e.Button;
                if (SFML.Window.Mouse.IsButtonPressed(button))
                {
                    OnMouseButtonDown(button);
                }
            };

            Game.Instance.Window.GetWindow.MouseButtonReleased += (s, e) =>
            {
                ResetMouse();
            };

            // --- keys
            Game.Instance.Window.GetWindow.KeyPressed += (s, e) =>
            {
                var key = (SFML.Window.Keyboard.Key)e.Code;

                if (SFML.Window.Keyboard.IsKeyPressed(key))
                {
                    OnKeyDown(key);
                }
            };

            Game.Instance.Window.GetWindow.KeyReleased += (s, e) =>
            {
                var key = (SFML.Window.Keyboard.Key)e.Code;

                if (keys.ContainsKey(key))
                {
                    OnKeyUp(key);
                }
            };
        }
        
        // --- joy

        /// <summary>
        /// initialise joy stick at id
        /// </summary>
        /// <param name="joyID">joy stick id</param>
        public void InitJoy(uint joyID)
        {
            if (SFML.Window.Joystick.IsConnected(joyID))
            {
                jsticks.Add((int)joyID, new JStick((int)joyID));
                jsInitialised = true;
            }
            else
            {
                Logger.Instance.Log.Information("NO JOYSTICKS CONNECTED");
                jsInitialised = false;
            }
        }

        public void Clean(){
            jsticks.Clear();            
        }

        public bool GetButtonState(int joy, uint button)
        {
            return jsticks[joy].GetButtonState(button);
        }

        public int GetXValue(int joy, int stick)
        {
            // x,y
            if (stick == 1)
            {
                return (int)jsticks[joy].GetXValue(1);
            }
            // y,u
            else if (stick == 2)
            {
                return (int)jsticks[joy].GetXValue(2);
            }
            return 0;
        }

        public int GetYValue(int joy, int stick)
        {
            // x,y
            if (stick == 1)
            {
                return (int)jsticks[joy].GetYValue(1);
            }
            // y,u
            else if (stick == 2)
            {
                return (int)jsticks[joy].GetYValue(2);
            }
            return 0;
        }


        // --- keys
        private void OnKeyDown(SFML.Window.Keyboard.Key k)
        {
            // var r = SFML.Window.Keyboard.IsKeyPressed(k);
            keys[k] = true;
        }

        private void OnKeyUp(SFML.Window.Keyboard.Key k)
        {
            keys[k] = false;
        }

        public bool IsPressed(SFML.Window.Keyboard.Key k)
        {
            // if not found add it 
            if (!keys.ContainsKey(k))
            {
                keys[k] = false;
            }

            return (keys[k] == true) ? true : false;
        }

        // --- mouse
        /*
        | MOUSE-CLICK |  Num  |
        | :---------- | :---: |
        | LEFT        |   0   |
        | RIGHT       |   1   |
        | MIDDLE      |   2   |
        **/

        /// <summary>
        /// get current mouse pos on screen
        /// </summary>
        /// <param name="x">mouse x pos</param>
        /// <param name="y">mouse y pos</param>
        private void OnMouseMove(int x, int y)
        {
            mouePos.X = x;
            mouePos.Y = y;
        }

        /// <summary>
        /// get mouse position
        /// </summary>
        /// <returns></returns>
        public Vector2f GetMousePosition()
        {
            return mouePos;
        }

        private void OnMouseButtonDown(SFML.Window.Mouse.Button button)
        {

            var value = (int)button;

            if (button == SFML.Window.Mouse.Button.Left)
            {
                mouseButtons[value] = true;
            }

            if (button == SFML.Window.Mouse.Button.Middle)
            {
                mouseButtons[value] = true;
            }

            if (button == SFML.Window.Mouse.Button.Right)
            {
                mouseButtons[value] = true;
            }
        }

        private void OnMouseButtonUp(SFML.Window.Mouse.Button button)
        {
            var value = (int)button;

            if (button == SFML.Window.Mouse.Button.Left)
            {
                mouseButtons[value] = false;
            }

            if (button == SFML.Window.Mouse.Button.Middle)
            {
                mouseButtons[value] = false;
            }

            if (button == SFML.Window.Mouse.Button.Right)
            {
                mouseButtons[value] = false;
            }
        }

        public bool GetMouseButtonState(int button)
        {
            return mouseButtons[button];
        }

        public void ResetMouse()
        {
            mouseButtons[(int)SFML.Window.Mouse.Button.Left] = false;
            mouseButtons[(int)SFML.Window.Mouse.Button.Right] = false;
            mouseButtons[(int)SFML.Window.Mouse.Button.Middle] = false;
        }
    }
    #endregion
}
