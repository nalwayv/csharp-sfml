using System;

namespace csharp_sfml
{
    public class Enemy : IGameObj
    {
        GameObject obj;

        public GameObject Obj { get => obj; set => obj = value; }

        public Enemy(){
            this.obj = new GameObject();
        }

        public void Load(LoadParams p)
        {
            obj.Load(p);
            obj.Radius = 20;
        }
        public void Clean()
        {
            obj.Clean();
        }

        public void Draw()
        {
            if(obj.IsActive)
            {
            obj.Draw();
            }
        }

        public void Update()
        {
            if(obj.IsActive)
            {
            obj.Update();

            }
        }
    }
}