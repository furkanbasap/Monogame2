using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Global;

namespace Monogame2.Scenes
{
    public class ScrollingBackground
    {
        private Vector2 screenpos, origin, texturesize;
        private Texture2D mytexture;
        private int screenheight;
        private int screenwidth;

        public void Load(Texture2D backgroundTexture)
        {
            mytexture = backgroundTexture;
            screenheight = 1440;
            screenwidth = 2400;

            // Set the origin so that we're drawing from the 
            // center of the top edge.
            origin = new Vector2(0, screenheight / 2);

            // Set the screen position to the center of the screen.
            screenpos = new Vector2(screenwidth / 2, screenheight / 2);

            // Offset to draw the second texture, when necessary.
            texturesize = new Vector2(mytexture.Width, 0);
            //texturesize = new Vector2(0, 960);

        }



        public void Update(float deltaX)
        {
            screenpos.X += deltaX;      
            screenpos.X %= mytexture.Width;
        }
        //public void Update(float deltaY)
        //{
        //    screenpos.Y += deltaY;
        //    screenpos.Y %= mytexture.Height;
        //}

        public void Draw(SpriteBatch batch, Color color)
        {
            // Draw the texture, if it is still onscreen.
            //if (screenpos.Y < screenheight)
            if (screenpos.X < screenwidth)
            {
                batch.Draw(mytexture, screenpos, null,
                     color, 0, origin, 1, SpriteEffects.None, 0f);
            }

            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            batch.Draw(mytexture, screenpos - texturesize, null,
                 color, 0, origin, 1, SpriteEffects.None, 0f);
        }
    }
}
