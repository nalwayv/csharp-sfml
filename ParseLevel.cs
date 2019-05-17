using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace csharp_sfml
{
    #region XMLDATA
    [Serializable()]
    [XmlRoot("root")]
    public class XMLData
    {
        [XmlElement("width")]
        public int Width { get; set; }

        [XmlElement("height")]
        public int Height { get; set; }

        [XmlElement("tilewidth")]
        public int Tilewidth { get; set; }

        [XmlArray("tilesets")]
        [XmlArrayItem("tileset", typeof(XMLTileSets))]
        public XMLTileSets[] TileSets { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("propertie", typeof(XMLProperties))]
        public XMLProperties[] Properties { get; set; }

        [XmlArray("layers")]
        [XmlArrayItem("layer", typeof(XMLLayers))]
        public XMLLayers[] Layers { get; set; }

        [XmlArray("objectgroups")]
        [XmlArrayItem("objectgroup", typeof(XMLObjectGroups))]
        public XMLObjectGroups[] ObjectGroups { get; set; }
    }

    [Serializable()]
    public class XMLTileSets
    {
        [XmlAttribute("firstgid")]
        public int FirstGID { get; set; }

        [XmlAttribute("id")]
        public string ID { get; set; }


        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }

        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("spacing")]
        public int Spacing { get; set; }

        [XmlAttribute("margin")]
        public int Margin { get; set; }

        [XmlAttribute("columns")]
        public int Columns { get; set; }

        [XmlAttribute("tilecount")]
        public int TileCount { get; set; }
    }

    [Serializable()]
    public class XMLLayers
    {
        [XmlElement("data")]
        public string Data { get; set; } // tilesheet data

        [XmlAttribute("id")]
        public int ID { get; set; } //

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("x")]
        public int X { get; set; }

        [XmlAttribute("y")]
        public int Y { get; set; }
    }

    [Serializable()]
    public class XMLProperties
    {
        [XmlAttribute("source")]
        public string Source { get; set; } // properties texture

        [XmlAttribute("id")]
        public string ID { get; set; } // name given within texture manager

        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    [Serializable()]
    public class XMLObjectGroups
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArray("objects")]
        [XmlArrayItem("object", typeof(XMLObj))]
        public XMLObj[] Objects { get; set; }
    }

    [Serializable()]
    public class XMLObj
    {
        // [XmlAttribute("name")]
        // public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("x")]
        public int X { get; set; }

        [XmlAttribute("y")]
        public int Y { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("callbackid")]
        public int CallbackID { get; set; }

        [XmlAttribute("active")]
        public bool Active { get; set; }

        [XmlAttribute("objtype")]
        public int ObjType { get; set; }

        [XmlAttribute("animated")]
        public bool Animated { get; set; }

        [XmlArray("animations")]
        [XmlArrayItem("animation", typeof(XMLAnimation))]
        public XMLAnimation[] Animations { get; set; }
    }

    [Serializable()]
    public class XMLAnimation
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("islooping")]
        public bool IsLooping { get; set; }

        [XmlAttribute("frames")]
        public int Frames { get; set; }

        [XmlAttribute("speed")]
        public float Speed { get; set; }

        [XmlAttribute("top")]
        public int Top { get; set; }

        [XmlAttribute("left")]
        public int Left { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }
    }
    #endregion XMLDATA

    #region LevelParser
    public class LevelParser
    {
        int tileSize;
        int width;
        int height;

        public LevelParser()
        {
            this.tileSize = 0;
            this.width = 0;
            this.height = 0;
        }

        /// <summary>
        ///   Load data from file
        /// </summary>
        public XMLData LoadData(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            var xs = new XmlSerializer(typeof(XMLData));

            var data = (XMLData)xs.Deserialize(fs);

            fs.Close();

            return data;
        }

        /// <summary>
        ///    Parse level file for its data
        /// </summary>
        /// <param name="fileName">path to file location</param>
        /// <returns>Level</returns>
        public Level ParseLevel(string fileName)
        {
            var data = LoadData(fileName);

            this.tileSize = data.Tilewidth;
            this.width = data.Width;
            this.height = data.Height;

            // new level
            var level = new Level();


            foreach (var val in data.Properties)
            {
                ParseProperties(val,level);
            }

            foreach (var val in data.TileSets)
            {
                ParseTileSets(val, level);
            }

            foreach (var val in data.Layers)
            {
                ParseTileLayers(val, level);
            }

            foreach (var val in data.ObjectGroups)
            {
                ParseObjLayers(val, level);
            }

            return level;
        }

        /// <summary>
        ///   parse tile sets
        /// </summary>
        private void ParseTileSets(XMLTileSets tileset, Level level)
        {

            // create new tile set 
            var ts = new TileSet(tileset.FirstGID,  // grid id                                 
                                tileset.Width,      // width                             
                                tileset.Height,     // height                                 
                                tileset.TileWidth,  // tile width                                 
                                tileset.TileHeight, // tile height                                     
                                tileset.Spacing,    // spacing                                 
                                tileset.Margin,     // margin                                 
                                tileset.Columns,    // spritesheets number of  columns                                  
                                tileset.ID);      // name                             

            // push to level tileset
            level.TileSets.Add(ts);
        }

        /// <summary>
        ///   Parse tile layers 
        /// </summary>
        private void ParseTileLayers(XMLLayers layer, Level level)
        {
            // parse string into list of int
            var tileData = (List<int>)layer.Data.Split(',').Select(Int32.Parse).ToList();

            // empty list;
            var data = new List<List<int>>();
            for (int i = 0; i < height; i++)
            {
                data.Add(new List<int>());
                for (int j = 0; j < width; j++)
                {
                    data[i].Add(0);
                }
            }

            // update with tile id number
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var id = (row * width) + col;
                    data[row][col] = tileData[id];
                }
            }

            var tilelayer = new TileLayer(
                tileSize,       // tile size
                level.TileSets,  // tile sets from parsed data acquired from ParseTileSets()
                data
            );

            // push
            level.Layers.Add(tilelayer);
        }

        /// <summary>
        ///   parse textures
        /// </summary>
        private void ParseProperties(XMLProperties prop, Level level)
        {
            if (prop.Type == "texture")
            {
                TextureHandler.Instance.Load(prop.Source, prop.ID);
                
                level.TextureIDs.Add(prop.ID);
            }
            
            if (prop.Type == "sound")
            {
                SoundHandler.Instance.Load(prop.Source, prop.ID, SoundType.Sound);
                level.SoundIDs.Add(prop.ID);
            }
        }

        /// <summary>
        ///   parse object layers
        /// </summary>
        private void ParseObjLayers(XMLObjectGroups groups, Level level)
        {
            // new obj layer
            var objLayer = new ObjectLayer();

            foreach (var v in groups.Objects)
            {
                var obj = Factory.Instance.Create(v.Type);
                var objAnimations = new List<Animation>();
                
                float animSpeed = 0.0f;
                int animFrames = 0;
                
                if(v.Animated){
                    foreach (var anim in v.Animations)
                    {
                        var a = new Animation(
                            anim.ID,
                            Time.FromSeconds(anim.Speed),
                            anim.IsLooping);

                        a.AddFrame(
                            new Vector2i(anim.Top, anim.Left),
                            new Vector2i(anim.Width, anim.Height),
                            anim.Frames);
                        
                        // keep ref 
                        animSpeed = anim.Speed;
                        animFrames = anim.Frames;

                        objAnimations.Add(a);
                    }
                }
                
                obj.Load(new LoadParams(v.ID,
                                        v.X,
                                        v.Y,
                                        v.Width,
                                        v.Height,
                                        objAnimations,
                                        v.CallbackID,
                                        v.Active,
                                        v.ObjType,
                                        v.Animated));

                // push to obj 
                objLayer.GameObjects.Add(obj);

                // push to level 
                level.Layers.Add(objLayer);

                if(v.Type == "Player"){
                    level.PtPlayer = (Player)obj;
                }

                if (v.Type == "Enemy"){
                    level.PtEnemy = (Enemy)obj;
                }
            }
        }
    }
    #endregion LevelParser
}
