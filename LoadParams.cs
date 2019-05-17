using System.Collections.Generic;

namespace csharp_sfml
{
    public class LoadParams
    {
        int objtype;
        int x;
        int y;
        int width;
        int height;
        int callBackID;
        string textureID;
        bool isActive;
        bool isAnimated;
        List<Animation> animations;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int CallBackID { get => callBackID; set => callBackID = value; }
        public string TextureID { get => textureID; set => textureID = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public int Objtype { get => objtype; set => objtype = value; }
        public bool IsAnimated { get => isAnimated; set => isAnimated = value; }
        public List<Animation> Animations { get => animations; set => animations = value; }

        public LoadParams(
            string textureID,
            int x,
            int y,
            int width,
            int height,
            List<Animation> anim,
            int callBackID,
            bool active,
            int objtype,
            bool isanimated)
        {
            this.textureID = textureID;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.callBackID = callBackID;
            this.animations = anim;
            this.isActive = active;
            this.objtype = objtype;
            this.isAnimated = isanimated;
        }
    }
}