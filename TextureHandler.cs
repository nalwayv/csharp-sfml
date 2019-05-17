using System;
using System.IO;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace csharp_sfml
{
    public class TextureHandler
    {
        /// <summary>
        ///     Store sprite and texture data
        /// </summary>
        struct TextureData
        {
            public Texture Texture { get; set; }
            public Sprite Sprite { get; set; }

            public TextureData(Texture tex, Sprite spr)
            {
                Texture = tex;
                Sprite = spr;
            }
        }

        Dictionary<string, TextureData> textureData;

        // --- Singleton
        private static readonly Lazy<TextureHandler> instance = new Lazy<TextureHandler>(() => new TextureHandler());
        public static TextureHandler Instance { get => instance.Value; }
        private TextureHandler()
        {
            textureData = new Dictionary<string, TextureData>();
        }
        // ---

        /// <summary>
        ///     Load a new texture from file into the texture map
        /// </summary>
        /// <param name="fileName">file location</param>
        /// <param name="id">id to give texture within map</param>
        public void Load(string fileName, string id)
        {
            try
            {
                if (textureData.ContainsKey(id))
                {
                    Logger.Instance.Log.Information($"TEXTURE '{id}' ALREADY ADDED");
                    return;
                }

                var texture = new Texture(fileName);

                // create sprite and store it with its texture
                var sprite = new Sprite();

                var td = new TextureData(texture, sprite);
                textureData.Add(id, td);

            }
            catch (FileNotFoundException e)
            {
                Logger.Instance.Log.Information($"TEXTURE FILE '{fileName}' NOT FOUND");
                throw new Exception($"{e}");
            }
        }

        /// <summary>
        ///     Clear from texture map
        /// </summary>
        /// <param name="id">id name for texture</param>
        public void ClearFromTextures(string id)
        {
            if (textureData.ContainsKey(id))
            {
                textureData.Remove(id);
            }
        }


        /// <summary>
        ///     Draw texture
        /// </summary>
        /// <param name="id">texture id</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="w">with</param>
        /// <param name="h">height</param>
        /// <param name="angle">angle</param>
        /// <param name="window">render window</param>
        public void Draw(string id,
                         int x,
                         int y,
                         int w,
                         int h,
                         float angle,
                         RenderWindow window)
        {
            var data = textureData[id];

            data.Sprite.Texture = data.Texture;

            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.TextureRect = new IntRect(0, 0, w, h); // part of the texture to render

            data.Sprite.Rotation = angle;

            window.Draw(data.Sprite);
        }

        /// <summary>
        ///     Draw a single frame
        /// </summary>
        /// <param name="id">texture id</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="currentrow">current row on tile sheet</param>
        /// <param name="currentframe">current frame on tile sheet</param>
        /// <param name="window">render window</param>
        public void DrawFrame(string id,
                            int x,
                            int y,
                            int w,
                            int h,
                            int currentrow,
                            int currentframe,
                            RenderWindow window)
        {
            var data = textureData[id];

            data.Sprite.Texture = data.Texture;

            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.TextureRect = new IntRect(
                w * currentframe,
                ((currentrow - 1) < 0) ? h * (currentrow) : h * (currentrow - 1), // so as to not time by -1
                w,
                h);

            window.Draw(data.Sprite);
        }


        /// <summary>
        ///     Draw animation used with animated sprites
        /// </summary>
        /// <param name="id">texture id</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="left">left destination position</param>
        /// <param name="top">right destination position</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="angle">angle</param>
        /// <param name="window">render window</param>
        public void DrawAnimation(string id,
                                int x,
                                int y,
                                int left,
                                int top,
                                int w,
                                int h,
                                float angle,
                                RenderWindow window)
        {
            var data = textureData[id];

            data.Sprite.Texture = data.Texture;

            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.TextureRect = new IntRect(left, top, w, h);

            data.Sprite.Rotation = angle;

            data.Sprite.Origin = new Vector2f(w /2, h /2f);

            window.Draw(data.Sprite);
        }

        /// <summary>
        ///     Draw tile
        /// </summary>
        /// <param name="id">texture id</param>
        /// <param name="margin">margin between tiles on sheet</param>
        /// <param name="spacing">spacing between tiles on sheet</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="w">with</param>
        /// <param name="h">height</param>
        /// <param name="currentrow">tile coords</param>
        /// <param name="currentframe">tile coords</param>
        /// <param name="window">render window</param>
        public void DrawTile(string id,
                            int margin,
                            int spacing,
                            int x,
                            int y,
                            int w,
                            int h,
                            int currentrow,
                            int currentframe,
                            RenderWindow window)
        {
            var data = textureData[id];

            data.Sprite.Texture = data.Texture;

            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.TextureRect = new IntRect(
                margin + (spacing + w) * currentframe,
                margin + (spacing + h) * currentrow,
                w,
                h);

            window.Draw(data.Sprite);
        }
    }
}
