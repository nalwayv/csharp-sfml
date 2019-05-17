using System;
namespace csharp_sfml
{
    class PlayState : IGameState
    {
        const string ID = "play";

        Level level;

        // TEMP

        public PlayState()
        {
        }

        public void Update()
        {

            // input
            if (InputHandler.Instance.IsJsInitialised)
            {
                if (InputHandler.Instance.GetButtonState(0, (int)Buttons.START))
                {
                    Game.Instance.StateMachine.PushState(new PauseState());
                }
            }
            else
            {
                if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Space))
                {
                    Game.Instance.StateMachine.PushState(new PauseState());
                }
            }

            if(level.PtPlayer.Obj.CollidesWith(level.PtEnemy.Obj))
            {
                // Console.WriteLine("Y|.");
            }

            level.Update();
        }

        public void Render()
        {
            level.Render();
        }

        public bool OnEnter()
        {
            // init level from parsed data
            var parseLevel = new LevelParser();

            // new level
            this.level = parseLevel.ParseLevel("data/level.xml");

            return true;
        }

        public bool OnExit()
        {
            foreach (var v in level.TextureIDs)
            {
                TextureHandler.Instance.ClearFromTextures(v);
            }

            foreach (var v in level.SoundIDs)
            {
                SoundHandler.Instance.ClearSound(v);
            }

            return true;
        }

        public string GetStateID()
        {
            return ID;
        }
    }
}
