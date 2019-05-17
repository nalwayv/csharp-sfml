namespace csharp_sfml
{
    public interface IGameState
    {
         void Update();
         void Render();
         bool OnEnter();
         bool OnExit();
         string GetStateID();
    }
}
