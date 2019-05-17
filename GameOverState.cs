using System;

namespace csharp_sfml
{
    class GameOverState:IMenuState
    {
        State state;
        const string ID = "gameover";

        public GameOverState(){

        }

        public string GetStateID()
        {
            return ID;
        }

        public bool OnEnter()
        {
            var parser = new StateParser();
            state = parser.ParseState("data/gameover.xml");

            state.Callbacks.Add(null);
            state.Callbacks.Add(ToMenuState);
            state.Callbacks.Add(Restart);

            SetCallBacks();

            return true;
        }

        public bool OnExit()
        {
             foreach(var v in state.Objects){
                v.Clean();
            }

            foreach(var v in state.TextureIDs){
                TextureHandler.Instance.ClearFromTextures(v);
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
            foreach( var obj in state.Objects){
                if(obj.GetType() == typeof(MenuButton)){
                    var button = (MenuButton)obj;
                    button.Sound = SoundHandler.Instance.ContainsSoundID("beep") ? "beep" : "";
                    var cb = state.Callbacks[button.CallbackID];
                    button.Callback = cb;
                }
            }
        }

        // --- callbacks

        public void ToMenuState(){
            Game.Instance.StateMachine.ChangeState(new MenuState());
        }

        public void Restart(){
            Game.Instance.StateMachine.ChangeState(new PlayState());
        }

    }
}