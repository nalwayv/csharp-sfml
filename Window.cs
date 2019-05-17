using SFML.Graphics;

namespace csharp_sfml
{
    public class Window
    {

        // ---
        bool isRunning;
        bool isFullscreen;
        string title;
        RenderWindow window;
        uint width;
        uint height;

        public bool IsRunning { get => isRunning; }
        public bool IsOpen { get => window.IsOpen; }
        public uint GetWidth { get => width; }
        public uint GetHeight { get => height; }
        public RenderWindow GetWindow { get => window; }


        public Window() { }

        public void Init(string title, uint width, uint height, bool fullscreen = false)
        {
            // settings
            this.title = title;
            this.width = width;
            this.height = height;
            this.isFullscreen = fullscreen;
            this.isRunning = true;

            // window
            var mode = new SFML.Window.VideoMode(this.width, this.height, 32);
            var style = this.isFullscreen ? SFML.Window.Styles.Fullscreen : SFML.Window.Styles.Default;

            this.window = new RenderWindow(mode, this.title, style);
        }

        public void HandleEvents()
        {
            if (InputHandler.Instance.IsPressed(SFML.Window.Keyboard.Key.Escape))
            {
                isRunning = false;
                window.Close();
            }
        }

        public void Update()
        {
            window.DispatchEvents();
            HandleEvents();
        }

        public void BeginDraw()
        {
            window.Clear(SFML.Graphics.Color.Black);
        }

        public void Draw()
        {
        }

        public void EndDraw()
        {
            window.Display();
        }

        public void Clean()
        {
            isRunning = false;
            window.Close();
        }
    }
}
