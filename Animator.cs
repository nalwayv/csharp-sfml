using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace csharp_sfml
{
    public class Animator{
        Time currentTime;
        List<Animation> animations;
        Animation currentAnimation;
        
        public Animator(List<Animation> animations){
            currentAnimation = null;
            
            currentTime = SFML.System.Time.Zero;
            
            this.animations = animations;

            // switch to last;
            Switch(animations[animations.Count-1]);
        }

        public void Clear(){
            animations.Clear();
        }

        public void Switch(Animation animation)
        {
            currentAnimation = animation;
            currentTime = SFML.System.Time.Zero;
        }

        public bool SwitchAnimation(string id)
        {
            var animation = FindAnimation(id);
            if (animation == null)
            {
                return false;
            }

            Switch(animation);
            return true;
        }

        private Animation FindAnimation(string id)
        {
            if(currentAnimation == null|| currentAnimation.Id == id)
            {
                return null;
            } 

            foreach (var a in animations)
            {
                if (a.Id == id)
                {
                    return a;
                }
            }
            return null;
        }

        public int NumberOfFrames()
        {
            if(currentAnimation == null) return -1;

            return currentAnimation.Frames.Count;
        }

        public float Speed()
        {
            if(currentAnimation == null) return -1;
            
            return currentAnimation.Duration.AsSeconds();
        }

        public IntRect UpdateAnim()
        {
            currentTime += Game.Instance.TimePerFrame;

            var scaleTime = currentTime.AsSeconds() / currentAnimation.Duration.AsSeconds();
            var numFrames = currentAnimation.Frames.Count;
            var currentFrame = (int)(scaleTime * numFrames);


            if (currentAnimation.IsLooping)
            {
                currentFrame %= numFrames;
            }
            else if (currentFrame >= numFrames)
            {
                currentFrame = (numFrames - 1);
            }

            var destRect = currentAnimation.Frames[currentFrame];
            
            return destRect;
        }
    }
}