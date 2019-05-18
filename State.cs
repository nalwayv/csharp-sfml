using System;
using System.Collections.Generic;

namespace csharp_sfml{
    public class State : ILayer
    {
        List<IGameObj> objects;
        List<Action> callbacks;

        // store for cleaning later
        List<string> textureIDs;
        List<string> soundIDs;

        public List<IGameObj> Objects { get => objects; set => objects = value; }
        public List<string> TextureIDs { get => textureIDs; set => textureIDs = value; }
        public List<Action> Callbacks { get => callbacks; set => callbacks = value; }
        public List<string> SoundIDs { get => soundIDs; set => soundIDs = value; }
        

        public State()
        {
            this.objects = new List<IGameObj>();
            this.textureIDs = new List<string>();
            this.callbacks = new List<Action>();
            this.soundIDs = new List<string>();
        }

        public void Render()
        {
            foreach(var obj in objects){
                obj.Draw();
            }
        }

        public void Update()
        {
            foreach(var obj in objects){
                obj.Update();
            }
        }
    }
}