using System;
using System.Collections.Generic;

namespace csharp_sfml
{
    public class MenuState : IMenuState
    {
        const string ID = "menu";

        State state;

        public MenuState()
        {
        }

        public string GetStateID()
        {
            return ID;
        }

        public bool OnEnter()
        {
            // parse data
            var parser = new StateParser();
            state = parser.ParseState("data/menu.xml");

            // callbacks
            state.Callbacks.Add(null);
            state.Callbacks.Add(ToPlayState);
            state.Callbacks.Add(ExitGame);

            // callbacks
            SetCallBacks();

            return true;
        }

        public bool OnExit()
        {
            foreach (var t in state.Objects)
            {
                t.Clean();
            }

            foreach (var t in state.TextureIDs)
            {
                TextureHandler.Instance.ClearFromTextures(t);
            }

            return true;
        }

        public void Render()
        {
            state.Render();
        }

        public void Update()
        {
            state.Update();
        }

        public void SetCallBacks()
        {
            foreach (var obj in state.Objects)
            {
                if (obj.GetType() == typeof(MenuButton))
                {
                    var button = (MenuButton)obj;
                    button.Sound = SoundHandler.Instance.ContainsSoundID("beep") ? "beep" : "";
                    var cb = state.Callbacks[button.CallbackID];//set to chosen function 
                    button.Callback = cb; // set that to be buttons callback
                }
            }
        }

        // -- callbacks

        void ToPlayState()
        {
            Game.Instance.StateMachine.ChangeState(new PlayState());
        }

        void ExitGame()
        {
            Game.Instance.Window.Clean();
        }
    }
}