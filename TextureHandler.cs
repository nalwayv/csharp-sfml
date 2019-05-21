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

                sprite.Texture = texture;


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
        public void Draw(string id,
                         int x,
                         int y,
                         int w,
                         int h,
                         float angle,
                         RenderWindow window)
        {
            var data = textureData[id];

            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.TextureRect = new IntRect(0, 0, w, h); // part of the texture to render
            
            data.Sprite.Origin = new Vector2f(0,0);

            data.Sprite.Rotation = angle;
            
            window.Draw(data.Sprite);
        }

        /// <summary>
        ///     Draw a single frame
        /// </summary>
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


            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.Origin = new Vector2f(0,0);

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


            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.TextureRect = new IntRect(left, top, w, h);

            data.Sprite.Rotation = angle;

            data.Sprite.Origin = new Vector2f(w*.5f,h*.5f);

            window.Draw(data.Sprite);
        }

        /// <summary>
        ///     Draw tile
        /// </summary>
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

            data.Sprite.Position = new Vector2f(x, y);

            data.Sprite.Origin = new Vector2f(0,0);

            data.Sprite.TextureRect = new IntRect(
                margin + (spacing + w) * currentframe,
                margin + (spacing + h) * currentrow,
                w,
                h);

            window.Draw(data.Sprite);
        }
    }
}
