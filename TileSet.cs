namespace csharp_sfml{
    public class TileSet{
        int gridID;
        int tileWidth;
        int tileHeight;
        int width;
        int height;
        int spacing;
        int margin;
        int numberOfColumns;
        string name;

        public int GridID{get=>gridID;}
        public int TileWidth{get=>TileWidth;}
        public int TileHeight{get=>tileHeight;}
        public int Width{get=>width;}
        public int Height{get=>height;}
        public int Spacing{get=>spacing;}
        public int Margin{get=>margin;}
        public int NumberOfColumns{get=>numberOfColumns;}
        public string Name{get=>name;}

        public TileSet()
        {
            this.gridID = 0;
            this.width = 0;
            this.height = 0;
            this.tileWidth = 0;
            this.tileHeight = 0;
            this.spacing = 0;
            this.margin = 0;
            this.numberOfColumns = 0;
            this.name = "";
        }

        public TileSet(int gridID,
                       int width,
                       int height,
                       int tileWidth,
                       int tileHeight,
                       int spacing,
                       int margin,
                       int numberOfColumns,
                       string name)
        {
            this.gridID = gridID;
            this.width = width;
            this.height = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.spacing = spacing;
            this.margin = margin;
            this.numberOfColumns = numberOfColumns;
            this.name = name;
        }
    }
}
