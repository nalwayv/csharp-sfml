using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SFML.System;

namespace csharp_sfml
{
    #region XMLDATA
    [Serializable()]
    [XmlRoot("root")]
    public class XMLState
    {
        [XmlArray("properties")]
        [XmlArrayItem("propertie", typeof(XMLStateProperties))]
        public XMLStateProperties[] Textures { get; set; }

        [XmlArray("objects")]
        [XmlArrayItem("object", typeof(XMLStateObjects))]
        public XMLStateObjects[] Objects { get; set; }
    }

    [Serializable()]
    public class XMLStateProperties
    {
        [XmlAttribute("source")]
        public string Source { get; set; }

        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    [Serializable()]
    public class XMLStateObjects
    {
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
        public int CallBackID { get; set; }

        [XmlAttribute("active")]
        public bool Active { get; set; }

        [XmlAttribute("objtype")]
        public int ObjType { get; set; }

        [XmlAttribute("animated")]
        public bool Animated { get; set; }


        [XmlArray("animations")]
        [XmlArrayItem("animation", typeof(XMLStateAnimation))]
        public XMLStateAnimation[] Animations { get; set; }

        // [XmlElement("speed")]
        // public float Speed{get;set;}

        // [XmlElement("frames")]
        // public int Frames{get;set;}
    }

    public class XMLStateAnimation
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

    #endregion

    #region XMLDATA
    public class StateParser
    {
        public StateParser()
        {
        }

        public XMLState LoadData(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            var xs = new XmlSerializer(typeof(XMLState));
            var data = (XMLState)xs.Deserialize(fs);
            fs.Close();

            return data;
        }

        public State ParseState(string fileName)
        {
            var data = LoadData(fileName);
            var state = new State();

            foreach (var val in data.Textures)
            {
                ParseProperties(val, state);
            }

            foreach (var val in data.Objects)
            {
                ParseObjects(val, state);
            }

            return state;
        }

        private void ParseProperties(XMLStateProperties xml, State state)
        {
            // add to texture handler
            if (xml.Type == "texture")
            {
                TextureHandler.Instance.Load(xml.Source, xml.ID);
                state.TextureIDs.Add(xml.ID);
            }

            if (xml.Type == "sound")
            {
                SoundHandler.Instance.Load(xml.Source, xml.ID, SoundType.Sound);
                state.SoundIDs.Add(xml.ID);
            }
        }

        private void ParseObjects(XMLStateObjects xml, State state)
        {
            var obj = Factory.Instance.Create(xml.Type);
            var objAnimations = new List<Animation>();

            float animSpeed = 0.0f;
            int animFrames = 0;

            if (xml.Animated)
            {
                foreach (var anim in xml.Animations)
                {
                    var a = new Animation(
                        anim.ID,
                        SFML.System.Time.FromSeconds(anim.Speed),
                        anim.IsLooping);

                    a.AddFrame(
                        new Vector2i(anim.Top, anim.Left),
                        new Vector2i(anim.Width, anim.Height),
                        anim.Frames);

                    animSpeed = anim.Speed;
                    animFrames = anim.Frames;
                    
                    objAnimations.Add(a);
                }
            }

            obj.Load(new LoadParams(
                xml.ID,
                xml.X,
                xml.Y,
                xml.Width,
                xml.Height,
                objAnimations,
                xml.CallBackID,
                xml.Active,
                xml.ObjType,
                xml.Animated));

            state.Objects.Add(obj);
        }
    }
    #endregion
}