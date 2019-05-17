using System;
using System.Collections.Generic;

namespace csharp_sfml
{
    public class ObjectLayer : ILayer
    {
        List<IGameObj> gameObjs;

        public List<IGameObj> GameObjects
        {
            get => gameObjs;

            set
            {
                if(gameObjs.Count != 0)
                {
                    gameObjs.Clear();
                }

                gameObjs = value;
            }
        }

        public ObjectLayer(){
            gameObjs = new List<IGameObj>();
        }

        /// <summary>
        ///   Render
        /// </summary>
        public void Render(){
            foreach(var v in gameObjs){
                v.Draw();
            }
        }

        /// <summary>
        ///   Update
        /// </summary>
        public void Update(){
            foreach(var v in gameObjs){
                v.Update();
            }
        }
    }
}
