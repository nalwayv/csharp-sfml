using System;
using System.Collections.Generic;
namespace csharp_sfml
{
    public class TileLayer : ILayer
    {
        int tileSize;
        int numColumns;
        int numRows;
        SFML.System.Vector2f position;
        SFML.System.Vector2f velocity;

        List<TileSet> tileSets;
        List<List<int>> tileIDs;

        public int TileSize { get => tileSize; set => tileSize = value; }

        // set up in parse level
        public List<List<int>> TileIDs { get => tileIDs; set => tileIDs = value; }

        public TileLayer(int tileSize, List<TileSet> tileSets, List<List<int>> tiledata)
        {
            this.tileSize = tileSize;

            this.tileSets = tileSets;

            this.position = new SFML.System.Vector2f(0, 0);

            this.velocity = new SFML.System.Vector2f(0, 0);

            this.tileIDs = tiledata; 

            this.numColumns = (int)Game.Instance.Window.GetWidth / this.tileSize;

            this.numRows = (int)Game.Instance.Window.GetHeight / this.tileSize;
        }

        public void Render()
        {
            var x1 = (int)position.X / tileSize;
            var y1 = (int)position.Y / tileSize;

            var x2 = (int)position.X % tileSize;
            var y2 = (int)position.Y % tileSize;

            // draw
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    // tile id number
                    var id = tileIDs[row + y1][col + x1];

                    if (id == 0)
                    {
                        continue;
                    }

                    var tileset = GetTileSetByID(id);

                    id--; // get real tile id 

                    // id / or % from spritesheet's number of columns
                    int tileX = (id - (tileset.GridID - 1)) / tileset.NumberOfColumns;
                    int tileY = (id - (tileset.GridID - 1)) % tileset.NumberOfColumns;

                    TextureHandler.Instance.DrawTile(
                        tileset.Name,                       // name
                        tileset.Margin,                     // margin
                        tileset.Spacing,                    // spacing
                        (col * tileSize) - x2,              // x
                        (row * tileSize) - y2,              // y
                        tileSize,                           // width
                        tileSize,                           // height
                        tileX,                              // column row
                        tileY,                              // column col
                        Game.Instance.Window.GetWindow);    // window
                }
            }
        }

        public void Update()
        {
            position += velocity;
        }

        public TileSet GetTileSetByID(int tileID)
        {
            int i = 1;
            for (; i < tileSets.Count; i++)
            {
                var current = tileSets[i - 1].GridID;
                var next = tileSets[i].GridID;

                // is in order ?
                if (tileID >= current && tileID < next)
                {
                    return tileSets[i];
                }
            }
            return tileSets[i - 1];
        }
    }
}
