using System;

namespace csharp_sfml
{
    public class PauseState : IMenuState
    {

        State state;

        const string ID = "pause";

        public PauseState()
        {
        }

        public string GetStateID()
        {
            return ID;
        }

        public bool OnEnter()
        {
            var parser = new StateParser();
            state = parser.ParseState("data/pause.xml");

            state.Callbacks.Add(null);//empty
            state.Callbacks.Add(ToMenuState);
            state.Callbacks.Add(Resume);

            SetCallBacks();

            return true;
        }

        public bool OnExit()
        {
            foreach(var t in state.Objects){
                t.Clean();
            }

            foreach(var t in state.TextureIDs){
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

                    // set callback
                    var cb = state.Callbacks[button.CallbackID];
                    button.Callback = cb;
                }
            }
        }

        // --- callbacks

        public void ToMenuState()
        {
            Game.Instance.StateMachine.ChangeState(new MenuState());
        }

        public void Resume()
        {
            Game.Instance.StateMachine.PopState();
        }
    }
}