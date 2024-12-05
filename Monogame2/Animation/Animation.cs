using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Animation
{
    public class Animation
    {
        public AnimationFrames CurrentFrame { get; set; }

        private List<AnimationFrames> frames;

        private int counter;

        private double frameMovement = 0;

        public Animation()
        {
            frames = new List<AnimationFrames>();
        }

        public void AddFrame(AnimationFrames animationFrames)
        {
            frames.Add(animationFrames);
            CurrentFrame = frames[0];
        }


        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            frameMovement += CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 10;

            if (frameMovement >= CurrentFrame.SourceRectangle.Width / fps)
            {
                counter++;
                frameMovement = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;
            for (int y = 0; y <= height - heightOfFrame; y += heightOfFrame)
            {
                for (int x = 0; x <= width - widthOfFrame; x += widthOfFrame)
                {
                    frames.Add(new AnimationFrames(
                   new Rectangle(x, y, widthOfFrame, heightOfFrame)));
                }
            }
        }
    }
}
