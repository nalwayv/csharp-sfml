using System;
using System.Collections.Generic;


namespace csharp_sfml{
    public class Factory{

        Dictionary<string,ICreator> objCreator;

        // ---
        private static readonly Lazy<Factory>instance = new Lazy<Factory>(()=>new Factory());

        public static Factory Instance{get => instance.Value;}

        private Factory(){
            objCreator = new Dictionary<string, ICreator>();
        }
        // ---

        public bool Register(string typeID, ICreator creator){
            if(objCreator.ContainsKey(typeID)){
                return false;
            }

            objCreator.Add(typeID,creator);
            return true;
        }

        public IGameObj Create(string typeID){
            if(!objCreator.ContainsKey(typeID)){
                throw new Exception("obj not found");
            }

            var c = (ICreator)objCreator[typeID];

            return c.CreateObj();

        }
    }
}
