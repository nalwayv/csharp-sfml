using System;
using System.Collections.Generic;

namespace csharp_sfml
{
    public class Level : ILayer
    {
        Player ptPlayer; // pointer to player obj within layers
        Enemy ptEnemy; // pointer to player obj within layers

        List<ILayer> layers;
        List<TileSet> tilesets;
        // store for cleaning later
        List<string> textureIDs;
        List<string> soundIDs;

        public List<TileSet> TileSets { get => tilesets; set => tilesets = value; }
        public List<ILayer> Layers { get => layers; set => layers = value; }
        public List<string> TextureIDs { get => textureIDs; set => textureIDs = value; }
        public List<string> SoundIDs { get => soundIDs; set => soundIDs = value; }
        public Player PtPlayer { get => ptPlayer; set => ptPlayer = value; }
        public Enemy PtEnemy { get => ptEnemy; set => ptEnemy = value; }

        public Level()
        {
            this.layers = new List<ILayer>();
            this.tilesets = new List<TileSet>();
            this.textureIDs = new List<string>();
            this.soundIDs = new List<string>();
        }

        public void Update()
        {
            foreach (var v in layers)
            {
                v.Update();
            }
        }

        public void Render()
        {
            foreach (var v in layers)
            {
                v.Render();
            }
        }
    }
}
