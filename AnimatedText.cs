using System;
namespace csharp_sfml
{
    public class AnimatedTexture : IGameObj
    {
        GameObject obj;

        public AnimatedTexture()
        {
            obj = new GameObject();
        }

        public void Load(LoadParams p)
        {
            obj.Load(p);
        }

        public void Clean()
        {
            obj.Clean();
        }

        public void Draw()
        {
            obj.Draw();
        }

        public void Update()
        {
            obj.Update();
        }
    }
}