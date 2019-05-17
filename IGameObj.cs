namespace csharp_sfml
{
    public interface IGameObj
    {
         void Load(LoadParams p);
         void Update();
         void Draw();
         void Clean();
    }
}