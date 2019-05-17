using System;

namespace csharp_sfml
{
    public enum MouseStates
    {
        MoueOut,
        MouseOver,
        MouseClicked
    }

    public class MenuButton : IGameObj
    {
        bool buttonReleased;
        int callbackID;
        GameObject obj;
        Action callback;
        string sound;

        public Action Callback { get => callback; set => callback = value; }
        public int CallbackID { get => callbackID; set => callbackID = value; }
        public string Sound { get => sound; set => sound = value; }

        public MenuButton()
        {
            obj = new GameObject();
        }

        public void Load(LoadParams p)
        {
            obj.Load(p);
            
            sound = "";

            callbackID = p.CallBackID;

            obj.CurrentFrame = (int)MouseStates.MoueOut;
        }

        public void Draw()
        {
            obj.Draw();
        }

        public void Update()
        {
            obj.Update();

            var mousePos = InputHandler.Instance.GetMousePosition();

            // mouseover
            if (mousePos.X < obj.Position.X + obj.Width && mousePos.X > obj.Position.X &&
                mousePos.Y < obj.Position.Y + obj.Height && mousePos.Y > obj.Position.Y)
            {
                obj.CurrentFrame = (int)MouseStates.MouseOver;

                // left mouse click
                if (InputHandler.Instance.GetMouseButtonState(0) && buttonReleased)
                {
                    obj.CurrentFrame = (int)MouseStates.MouseClicked;

                    // loaded from parsed info
                    SoundHandler.Instance.PlaySound(Sound);

                    callback(); // call set callback;

                    buttonReleased = false;
                }
                else if (!InputHandler.Instance.GetMouseButtonState(0))
                {
                    buttonReleased = true;

                    obj.CurrentFrame = (int)MouseStates.MouseOver;
                }
            }
            else
            {
                obj.CurrentFrame = (int)MouseStates.MoueOut;
            }
        }

        public void Clean()
        {
            obj.Clean();
        }

    }
}