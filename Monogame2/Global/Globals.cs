using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame2.Global
{
    public class Globals
    {
        public static float TotalSeconds { get; set; }
        public static ContentManager content { get; set; }
        public static SpriteBatch spriteBatch { get; set; }

        public static void Update(GameTime gt)
        {
            TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
