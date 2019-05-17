using System;

namespace csharp_sfml
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Log.Information("START");

            Game.Instance.Init("SFML", 640, 450);
            Game.Instance.HandleEvents();

            while (Game.Instance.Window.IsOpen || Game.Instance.Window.IsRunning)
            {
                Game.Instance.RestartClock();

                while (Game.Instance.ElapsedTime > Game.Instance.TimePerFrame)
                {
                    Game.Instance.ElapsedTime -= Game.Instance.TimePerFrame;
                    Game.Instance.Update();
                }
                
                Game.Instance.Render();
            }

            Game.Instance.Clean();

            Logger.Instance.Log.Information("END");
        }
    }
}
