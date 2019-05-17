using System;
using SFML.System;
using SFML.Graphics;

namespace csharp_sfml
{
    public class Game
    {

        // --- Singleton
        private static readonly Lazy<Game> instance = new Lazy<Game>(() => new Game());
        public static Game Instance { get => instance.Value;  }
        private Game() { }
        // ---

        const float FPS = 60.0f;
        uint width;
        uint height;
        string title;
        Time elapsedTime;
        Time timePerFrame;
        Clock clock;
        Window window;
        StateMachine stateMachine;

        public Time ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
        public Time TimePerFrame { get => timePerFrame; set => timePerFrame = value; }
        public Clock Clock { get => clock; private set => clock = value; }
        public Window Window { get => window; private set => window = value; }

        public StateMachine StateMachine { get => stateMachine; private set => stateMachine = value; }
        // ---

        // ---
        /// <summary>
        ///   Init a new game
        /// </summary>
        public void Init(string title, uint width, uint height)
        {
            // setup
            this.title = title;
            this.width = width;
            this.height = height;

            // window
            this.window = new Window();
            this.window.Init(this.title, this.width, this.height);

            // time
            this.elapsedTime = Time.Zero;
            this.timePerFrame = Time.FromSeconds(1 / FPS);
            this.clock = new Clock();

            // joystick
            SFML.Window.Joystick.Update();
            InputHandler.Instance.InitJoy(0);

            // add bank obj to factory
            Factory.Instance.Register("Player", new CreatePlayer());
            Factory.Instance.Register("Enemy", new CreateEnemy());
            Factory.Instance.Register("Button", new CreateMenuButton());
            Factory.Instance.Register("AnimatedText", new CreateAnimatedText());

            // stateMachine
            this.stateMachine = new StateMachine();
            this.stateMachine.ChangeState(new MenuState());
        }

        public void RestartClock()
        {
            elapsedTime += clock.Restart();
        }

        public void HandleEvents()
        {
            InputHandler.Instance.Update();
        }

        public void Update()
        {

            stateMachine.Update();
            window.Update();
        }

        public void Render()
        {
            window.BeginDraw();
            // -->
            stateMachine.Render();
            // <--
            window.EndDraw();
        }

        public void Clean()
        {
            InputHandler.Instance.Clean();
            window.Clean();
        }
    }
}
