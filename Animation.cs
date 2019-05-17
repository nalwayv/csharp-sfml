using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace csharp_sfml
{
    public class Animation
    {
        string id;
        List<IntRect> frames;
        SFML.System.Time duration;
        bool isLooping;

        public string Id { get => id; set => id = value; }
        public List<IntRect> Frames { get => frames; set => frames = value; }
        public Time Duration { get => duration; set => duration = value; }
        public bool IsLooping { get => isLooping; set => isLooping = value; }
        public float Speed {get => duration.AsSeconds();}

        public Animation(string id, Time duration, bool isLooping)
        {
            this.id = id;

            this.frames = new List<IntRect>();
            this.duration = duration;
            this.isLooping = isLooping;
        }

        public void AddFrame(Vector2i startFrom, Vector2i frameSize, int totalFrames)
        {
            var current = startFrom;

            for (var i = 0; i < totalFrames; i++)
            {
                var newFrame = new IntRect(
                    current.X,
                    current.Y,
                    frameSize.X,
                    frameSize.Y);

                this.frames.Add(newFrame);

                // move along
                current.X += frameSize.X;
            }
        }
    }
}